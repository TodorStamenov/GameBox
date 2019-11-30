import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Store, select } from '@ngrx/store';

import { GameService } from '../../services/game.service';
import { IGameBindingModel } from '../../models/game-binding.model';
import { IState } from '../../+store/games.state';
import { filter, takeWhile } from 'rxjs/operators';
import { LoadGameById } from '../../+store/games.actions';

@Component({
  selector: 'app-delete-game',
  templateUrl: './delete-game.component.html'
})
export class DeleteGameComponent implements OnInit, OnDestroy {
  private componentActive = true;

  public gameId: string;
  public deleteGameForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private gameService: GameService,
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<IState>
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.store.dispatch(new LoadGameById(this.gameId));

    this.deleteGameForm = this.fb.group({
      'title': new FormControl({ value: '', disabled: true }),
      'description': new FormControl({ value: '', disabled: true }),
      'thumbnailUrl': new FormControl({ value: '', disabled: true }),
      'price': new FormControl({ value: '', disabled: true }),
      'size': new FormControl({ value: '', disabled: true }),
      'videoId': new FormControl({ value: '', disabled: true }),
      'releaseDate': new FormControl({ value: '', disabled: true }),
      'categoryId': new FormControl({ value: '', disabled: true })
    });

    this.store.pipe(
      select(s => s.games.byId),
      filter(g => !!g),
      takeWhile(() => this.componentActive)
    )
    .subscribe((game: IGameBindingModel) => this.setupDeleteForm(game));
  }

  public ngOnDestroy(): void {
    this.componentActive = false;
  }

  public deleteGame(): void {
    this.gameService.deleteGame$(this.gameId)
      .subscribe(() => this.router.navigate(['/games/all']));
  }

  private setupDeleteForm(game: IGameBindingModel): void {
    this.deleteGameForm.setValue({
      title: game.title,
      description: game.description,
      thumbnailUrl: game.thumbnailUrl,
      price: game.price,
      size: game.size,
      videoId: game.videoId,
      releaseDate: game.releaseDate,
      categoryId: game.categoryId
    });
  }
}

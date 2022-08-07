import { Component, OnInit, OnDestroy } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Store, select } from '@ngrx/store';

import { filter, takeWhile } from 'rxjs/operators';

import { GameService } from '../../../../services/game.service';
import { IGameBindingModel } from '../../models/game-binding.model';
import { IState } from '../../+store/games.state';
import { LoadGameById } from '../../+store/games.actions';
import { LoadCategoryNames } from 'src/app/store/+store/category/categories.actions';

@Component({
  selector: 'app-delete-game',
  templateUrl: './delete-game.component.html'
})
export class DeleteGameComponent implements OnInit, OnDestroy {
  private componentActive = true;

  public gameId: string;
  public deleteGameForm: UntypedFormGroup;

  constructor(
    private fb: UntypedFormBuilder,
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
      'title': new UntypedFormControl({ value: '', disabled: true }),
      'description': new UntypedFormControl({ value: '', disabled: true }),
      'thumbnailUrl': new UntypedFormControl({ value: '', disabled: true }),
      'price': new UntypedFormControl({ value: '', disabled: true }),
      'size': new UntypedFormControl({ value: '', disabled: true }),
      'videoId': new UntypedFormControl({ value: '', disabled: true }),
      'releaseDate': new UntypedFormControl({ value: '', disabled: true }),
      'categoryId': new UntypedFormControl({ value: '', disabled: true })
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
      .subscribe(() => {
        this.router.navigate(['/games/all']);
        this.store.dispatch(new LoadCategoryNames());
      });
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

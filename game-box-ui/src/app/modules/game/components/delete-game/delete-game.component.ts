import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Store, select } from '@ngrx/store';

import { GameService } from '../../services/game.service';
import { IAppState } from 'src/app/store/app.state';
import { IGameBindingModel } from '../../models/game-binding.model';

@Component({
  selector: 'app-delete-game',
  templateUrl: './delete-game.component.html'
})
export class DeleteGameComponent implements OnInit {
  public gameId: string;

  public deleteGameForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private gameService: GameService,
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<IAppState>
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
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

    this.gameService
      .getGame$(this.gameId)
      .subscribe(() => {
        this.store.pipe(
          select(state => state.games.toEdit)
        )
        .subscribe((game: IGameBindingModel) => this.deleteGameForm.setValue({
          title: game.title,
          description: game.description,
          thumbnailUrl: game.thumbnailUrl,
          price: game.price,
          size: game.size,
          videoId: game.videoId,
          releaseDate: game.releaseDate,
          categoryId: game.categoryId
        }));
      });
  }

  public deleteGame(): void {
    this.gameService
      .deleteGame$(this.gameId)
      .subscribe(() => this.router.navigate(['/games/all']));
  }
}

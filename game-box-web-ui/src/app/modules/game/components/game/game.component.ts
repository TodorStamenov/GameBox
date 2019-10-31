import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { filter, takeWhile } from 'rxjs/operators';

import { IAppState } from 'src/app/store/app.state';
import { GameService } from '../../services/game.service';
import { FormService } from 'src/app/modules/core/services/form.service';
import { ActionType } from '../../../core/enums/action-type.enum';
import { IGameBindingModel } from '../../models/game-binding.model';
import { ICategoryMenuModel } from '../../../category/models/category-menu.model';
import { LoadCategoryNames } from 'src/app/store/categories/categories.actions';
import { LoadGameToEdit } from 'src/app/store/games/games.actions';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html'
})
export class GameComponent implements OnInit, OnDestroy {
  private componentActive = true;
  private gameId: string | undefined;
  private actionType: ActionType | undefined;

  public categories$: Observable<ICategoryMenuModel[]>;
  public gameForm: FormGroup;

  get title() { return this.gameForm.get('title'); }
  get description() { return this.gameForm.get('description'); }
  get thumbnailUrl() { return this.gameForm.get('thumbnailUrl'); }
  get price() { return this.gameForm.get('price'); }
  get size() { return this.gameForm.get('size'); }
  get videoId() { return this.gameForm.get('videoId'); }
  get releaseDate() { return this.gameForm.get('releaseDate'); }
  get categoryId() { return this.gameForm.get('categoryId'); }

  constructor(
    private fb: FormBuilder,
    private gameService: GameService,
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<IAppState>,
    public formService: FormService
  ) {
    this.actionType = ActionType[<string>this.route.snapshot.params['action']];
    this.gameId = this.route.snapshot.queryParams['id'];
  }

  public ngOnInit(): void {
    this.store.dispatch(new LoadCategoryNames());
    this.categories$ = this.store.pipe(
      select(s => s.categories.names)
    );

    this.gameForm = this.fb.group({
      'title': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      'description': [null, [Validators.required, Validators.minLength(20)]],
      'thumbnailUrl': [null],
      'price': [null, [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]],
      'size': [null, [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]],
      'videoId': [null, [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      'releaseDate': [null, [Validators.required]],
      'categoryId': [null]
    });

    if (this.actionType === ActionType.edit) {
      this.store.dispatch(new LoadGameToEdit(this.gameId));
      this.store.pipe(
        select(s => s.games.toEdit),
        filter(g => !!g),
        takeWhile(() => this.componentActive)
      )
      .subscribe(game => this.setupEditForm(game));
    }
  }

  public ngOnDestroy(): void {
    this.componentActive = false;
  }

  public saveGame(): void {
    if (this.actionType === ActionType.create) {
      this.gameService
        .addGame$(this.gameForm.value)
        .subscribe(() => this.navigateToHome());
    } else if (this.actionType === ActionType.edit) {
      this.gameService
        .editGame$(this.gameId, this.gameForm.value)
        .subscribe(() => this.navigateToHome());
    }
  }

  private navigateToHome(): void {
    this.router.navigate(['/games/all']);
  }

  private setupEditForm(game: IGameBindingModel) {
    this.gameForm.setValue({
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
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { Store, select } from '@ngrx/store';

import { Observable } from 'rxjs';
import { filter, takeWhile } from 'rxjs/operators';

import { GameService } from '../../services/game.service';
import { FormService } from 'src/app/modules/core/services/form.service';
import { ActionType } from '../../../core/enums/action-type.enum';
import { IGameBindingModel } from '../../models/game-binding.model';
import { ICategoryMenuModel } from '../../../category/models/category-menu.model';
import { LoadGameById } from 'src/app/modules/game/+store/games.actions';
import { IState } from '../../+store/games.state';
import { LoadCategoryNames } from 'src/app/store/+store/category/categories.actions';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html'
})
export class GameComponent implements OnInit, OnDestroy {
  public categories$: Observable<ICategoryMenuModel[]>;
  public gameForm: FormGroup;

  private componentActive = true;
  private gameId: string | undefined;
  private actionType: ActionType | undefined;

  constructor(
    private fb: FormBuilder,
    private gameService: GameService,
    private router: Router,
    private route: ActivatedRoute,
    private store: Store<IState>,
    public formService: FormService
  ) {
    this.actionType = ActionType[<string>this.route.snapshot.params['action']];
    this.gameId = this.route.snapshot.queryParams['id'];
  }

  get title(): AbstractControl {
    return this.gameForm.get('title');
  }

  get description(): AbstractControl {
    return this.gameForm.get('description');
  }

  get thumbnailUrl(): AbstractControl {
    return this.gameForm.get('thumbnailUrl');
  }

  get price(): AbstractControl {
    return this.gameForm.get('price');
  }

  get size(): AbstractControl {
    return this.gameForm.get('size');
  }

  get videoId(): AbstractControl {
    return this.gameForm.get('videoId');
  }

  get releaseDate(): AbstractControl {
    return this.gameForm.get('releaseDate');
  }

  get categoryId(): AbstractControl {
    return this.gameForm.get('categoryId');
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
      this.store.dispatch(new LoadGameById(this.gameId));
      this.store.pipe(
        select(s => s.games.byId),
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
      this.gameService.addGame$(this.gameForm.value)
        .subscribe(() => this.navigateToHome());
    } else if (this.actionType === ActionType.edit) {
      this.gameService.editGame$(this.gameId, this.gameForm.value)
        .subscribe(() => this.navigateToHome());
    }
  }

  private navigateToHome(): void {
    this.router.navigate(['/games/all']);
  }

  private setupEditForm(game: IGameBindingModel): void {
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

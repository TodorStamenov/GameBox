import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { GameService } from '../../../services/game.service';
import { CategoryService } from '../../../services/category.service';
import { FormService } from 'src/app/modules/core/services/form.service';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html'
})
export class CreateGameComponent implements OnInit {
  public categories$ = this.categoryService.getCategoryNames$();
  public addGameForm: FormGroup;

  get title() { return this.addGameForm.get('title'); }
  get description() { return this.addGameForm.get('description'); }
  get thumbnailUrl() { return this.addGameForm.get('thumbnailUrl'); }
  get price() { return this.addGameForm.get('price'); }
  get size() { return this.addGameForm.get('size'); }
  get videoId() { return this.addGameForm.get('videoId'); }
  get releaseDate() { return this.addGameForm.get('releaseDate'); }
  get categoryId() { return this.addGameForm.get('categoryId'); }

  constructor(
    private fb: FormBuilder,
    private gameService: GameService,
    private categoryService: CategoryService,
    private router: Router,
    public formService: FormService
  ) { }

  public ngOnInit(): void {
    this.addGameForm = this.fb.group({
      'title': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      'description': [null, [Validators.required, Validators.minLength(20)]],
      'thumbnailUrl': [null],
      'price': [null, [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]],
      'size': [null, [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]],
      'videoId': [null, [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      'releaseDate': [null, [Validators.required]],
      'categoryId': [null]
    });
  }

  public addGame(): void {
    this.gameService
      .addGame$(this.addGameForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}

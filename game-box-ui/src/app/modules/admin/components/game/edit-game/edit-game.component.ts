import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { GameService } from '../../../services/game.service';
import { CategoryService } from '../../../services/category.service';
import { FormService } from 'src/app/modules/core/services/form.service';

@Component({
  selector: 'app-edit-game',
  templateUrl: './edit-game.component.html'
})
export class EditGameComponent implements OnInit {
  public gameId: string;
  public categories$ = this.categoryService.getCategoryNames$();
  public editGameForm: FormGroup;

  get title() { return this.editGameForm.get('title'); }
  get description() { return this.editGameForm.get('description'); }
  get thumbnailUrl() { return this.editGameForm.get('thumbnailUrl'); }
  get price() { return this.editGameForm.get('price'); }
  get size() { return this.editGameForm.get('size'); }
  get videoId() { return this.editGameForm.get('videoId'); }
  get releaseDate() { return this.editGameForm.get('releaseDate'); }
  get categoryId() { return this.editGameForm.get('categoryId'); }

  constructor(
    private fb: FormBuilder,
    private gameService: GameService,
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    public formService: FormService
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.editGameForm = this.fb.group({
      'title': [null, [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      'description': [null, [Validators.required, Validators.minLength(20)]],
      'thumbnailUrl': [null],
      'price': [null, [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]],
      'size': [null, [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]],
      'videoId': [null, [Validators.required, Validators.minLength(11), Validators.maxLength(11)]],
      'releaseDate': [null, [Validators.required]],
      'categoryId': [null]
    });

    this.gameService
      .getGame$(this.gameId)
      .subscribe(res => this.editGameForm.setValue({
        title: res.title,
        description: res.description,
        thumbnailUrl: res.thumbnailUrl,
        price: res.price,
        size: res.size,
        videoId: res.videoId,
        releaseDate: res.releaseDate,
        categoryId: res.categoryId
      }));
  }

  public editGame(): void {
    this.gameService
      .editGame$(this.gameId, this.editGameForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}

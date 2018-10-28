import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators, AbstractControl } from '@angular/forms';
import { Router } from '@angular/router';
import { GameService } from '../../../services/game.service';
import { CategoryMenuModel } from '../../../models/categories/category-menu.model';
import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html'
})
export class CreateGameComponent implements OnInit {
  public addGameForm: FormGroup;
  public categories: CategoryMenuModel[];

  public titleMessage: string;
  public descriptionMessage: string;
  public priceMessage: string;
  public sizeMessage: string;
  public videoIdMessage: string;
  public releaseDateMessage: string;

  private validationMessages = {
    titleValidationMessage: {
      required: 'Title is required!',
      minlength: 'Title should be at least 3 symbols long!',
      maxlength: 'Title should be less than 100 symbols long!'
    },
    descriptionMessage: {
      required: 'Description is required!',
      minlength: 'Description should be at least 20 symbols long!'
    },
    priceMessage: {
      required: 'Price is required!',
      min: 'Price should be positive real number!',
      max: 'Price should be positive real number!',
    },
    sizeMessage: {
      required: 'Size is required!',
      min: 'Size should be positive real number!',
      max: 'Size should be positive real number!',
    },
    videoIdMessage: {
      required: 'Video Id is required!',
      minlength: 'Video Id should be exactly 11 symbols long!',
      maxlength: 'Video Id should be exactly 11 symbols long!'
    },
    releaseDateMessage: {
      required: 'Release Date is required!'
    }
  };

  constructor(
    private fb: FormBuilder,
    private gameService: GameService,
    private categoryService: CategoryService,
    private router: Router
  ) { }

  ngOnInit() {
    this.categoryService
      .getCategoryNames()
      .subscribe(res => this.categories = res);

    this.addGameForm = this.fb.group({
      title: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]),
      description: new FormControl('', [Validators.required, Validators.minLength(20)]),
      thumbnailUrl: new FormControl(''),
      price: new FormControl('', [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]),
      size: new FormControl('', [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]),
      videoId: new FormControl('', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]),
      releaseDate: new FormControl('', [Validators.required]),
      categoryId: new FormControl('none')
    });

    const titleControl = this.addGameForm.controls.title;
    titleControl
      .valueChanges
      .subscribe(() => {
        this.titleMessage = '';
        this.titleMessage = this.setMessage(titleControl, 'titleValidationMessage');
      });

    const descriptionControl = this.addGameForm.controls.description;
    descriptionControl
      .valueChanges
      .subscribe(() => {
        this.descriptionMessage = '';
        this.descriptionMessage = this.setMessage(descriptionControl, 'descriptionMessage');
      });

    const priceControl = this.addGameForm.controls.price;
    priceControl
      .valueChanges
      .subscribe(() => {
        this.priceMessage = '';
        this.priceMessage = this.setMessage(priceControl, 'priceMessage');
      });
    
    const sizeControl = this.addGameForm.controls.size;
    sizeControl
      .valueChanges
      .subscribe(() => {
        this.sizeMessage = '';
        this.sizeMessage = this.setMessage(sizeControl, 'sizeMessage');
      });
    
    const videoIdControl = this.addGameForm.controls.videoId;
    videoIdControl
      .valueChanges
      .subscribe(() => {
        this.videoIdMessage;
        this.videoIdMessage = this.setMessage(videoIdControl, 'videoIdMessage');
      });

    const releaseDateControl = this.addGameForm.controls.releaseDate;
    releaseDateControl
      .valueChanges
      .subscribe(() => {
        this.releaseDateMessage = '';
        this.releaseDateMessage = this.setMessage(releaseDateControl, 'releaseDateMessage');
      });
  }

  setMessage(control: AbstractControl, messageKey: string): string {
    if ((control.touched || control.dirty) && control.errors) {
      return Object.keys(control.errors)
        .map(key => this.validationMessages[messageKey][key])
        .join(' ');
    }
  }

  addGame(): void {
    this.gameService
      .addGame(this.addGameForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}
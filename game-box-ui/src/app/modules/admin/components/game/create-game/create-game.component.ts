import { Router } from '@angular/router';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { Subscription, Observable } from 'rxjs';

import { GameService } from '../../../services/game.service';
import { CategoryService } from '../../../services/category.service';
import { CategoryMenuModel } from '../../../models/categories/category-menu.model';
import { FormService } from 'src/app/sharedServices/form.service';

@Component({
  selector: 'app-create-game',
  templateUrl: './create-game.component.html'
})
export class CreateGameComponent implements OnInit, OnDestroy {
  private titleSubscription: Subscription;
  private descriptionSubscription: Subscription;
  private priceSubscription: Subscription;
  private sizeSubscription: Subscription;
  private videoIdSubscription: Subscription;
  private releaseDateSubscription: Subscription;

  public addGameForm: FormGroup;
  public categories$: Observable<CategoryMenuModel[]>;

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
    private router: Router,
    private formService: FormService
  ) { }

  public ngOnInit(): void {
    this.categories$ = this.categoryService.getCategoryNames();

    this.addGameForm = this.fb.group({
      'title': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]),
      'description': new FormControl('', [Validators.required, Validators.minLength(20)]),
      'thumbnailUrl': new FormControl(''),
      'price': new FormControl('', [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]),
      'size': new FormControl('', [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]),
      'videoId': new FormControl('', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]),
      'releaseDate': new FormControl('', [Validators.required]),
      'categoryId': new FormControl('none')
    });

    const titleControl = this.addGameForm.controls.title;
    this.titleSubscription = titleControl
      .valueChanges
      .subscribe(() => {
        this.titleMessage = '';
        this.titleMessage = this.formService.setMessage(titleControl, 'titleValidationMessage', this.validationMessages);
      });

    const descriptionControl = this.addGameForm.controls.description;
    this.descriptionSubscription = descriptionControl
      .valueChanges
      .subscribe(() => {
        this.descriptionMessage = '';
        this.descriptionMessage = this.formService.setMessage(descriptionControl, 'descriptionMessage', this.validationMessages);
      });

    const priceControl = this.addGameForm.controls.price;
    this.priceSubscription = priceControl
      .valueChanges
      .subscribe(() => {
        this.priceMessage = '';
        this.priceMessage = this.formService.setMessage(priceControl, 'priceMessage', this.validationMessages);
      });

    const sizeControl = this.addGameForm.controls.size;
    this.sizeSubscription = sizeControl
      .valueChanges
      .subscribe(() => {
        this.sizeMessage = '';
        this.sizeMessage = this.formService.setMessage(sizeControl, 'sizeMessage', this.validationMessages);
      });

    const videoIdControl = this.addGameForm.controls.videoId;
    this.videoIdSubscription = videoIdControl
      .valueChanges
      .subscribe(() => {
        this.videoIdMessage = '';
        this.videoIdMessage = this.formService.setMessage(videoIdControl, 'videoIdMessage', this.validationMessages);
      });

    const releaseDateControl = this.addGameForm.controls.releaseDate;
    this.releaseDateSubscription = releaseDateControl
      .valueChanges
      .subscribe(() => {
        this.releaseDateMessage = '';
        this.releaseDateMessage = this.formService.setMessage(releaseDateControl, 'releaseDateMessage', this.validationMessages);
      });
  }

  public ngOnDestroy(): void {
    this.titleSubscription.unsubscribe();
    this.descriptionSubscription.unsubscribe();
    this.priceSubscription.unsubscribe();
    this.sizeSubscription.unsubscribe();
    this.videoIdSubscription.unsubscribe();
    this.releaseDateSubscription.unsubscribe();
  }

  public addGame(): void {
    this.gameService
      .addGame(this.addGameForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}

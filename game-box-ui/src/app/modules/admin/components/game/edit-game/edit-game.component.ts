import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { Subscription } from 'rxjs';

import { GameService } from '../../../services/game.service';
import { CategoryService } from '../../../services/category.service';
import { FormService } from 'src/app/modules/core/sharedServices/form.service';

@Component({
  selector: 'app-edit-game',
  templateUrl: './edit-game.component.html'
})
export class EditGameComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();

  public gameId: string;
  public categories$ = this.categoryService.getCategoryNames$();
  public editGameForm: FormGroup;

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
    private route: ActivatedRoute,
    private formService: FormService
  ) {
    this.gameId = this.route.snapshot.params['id'];
  }

  public ngOnInit(): void {
    this.editGameForm = this.fb.group({
      'title': new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]),
      'description': new FormControl('', [Validators.required, Validators.minLength(20)]),
      'thumbnailUrl': new FormControl(''),
      'price': new FormControl('', [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]),
      'size': new FormControl('', [Validators.required, Validators.min(0.01), Validators.max(Number.MAX_VALUE)]),
      'videoId': new FormControl('', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]),
      'releaseDate': new FormControl('', [Validators.required]),
      'categoryId': new FormControl()
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

    const titleControl = this.editGameForm.controls.title;
    this.subscription.add(titleControl
      .valueChanges
      .subscribe(() => {
        this.titleMessage = '';
        this.titleMessage = this.formService.setMessage(titleControl, 'titleValidationMessage', this.validationMessages);
      }));

    const descriptionControl = this.editGameForm.controls.description;
    this.subscription.add(descriptionControl
      .valueChanges
      .subscribe(() => {
        this.descriptionMessage = '';
        this.descriptionMessage = this.formService.setMessage(descriptionControl, 'descriptionMessage', this.validationMessages);
      }));

    const priceControl = this.editGameForm.controls.price;
    this.subscription.add(priceControl
      .valueChanges
      .subscribe(() => {
        this.priceMessage = '';
        this.priceMessage = this.formService.setMessage(priceControl, 'priceMessage', this.validationMessages);
      }));

    const sizeControl = this.editGameForm.controls.size;
    this.subscription.add(sizeControl
      .valueChanges
      .subscribe(() => {
        this.sizeMessage = '';
        this.sizeMessage = this.formService.setMessage(sizeControl, 'sizeMessage', this.validationMessages);
      }));

    const videoIdControl = this.editGameForm.controls.videoId;
    this.subscription.add(videoIdControl
      .valueChanges
      .subscribe(() => {
        this.videoIdMessage = '';
        this.videoIdMessage = this.formService.setMessage(videoIdControl, 'videoIdMessage', this.validationMessages);
      }));

    const releaseDateControl = this.editGameForm.controls.releaseDate;
    this.subscription.add(releaseDateControl
      .valueChanges
      .subscribe(() => {
        this.releaseDateMessage = '';
        this.releaseDateMessage = this.formService.setMessage(releaseDateControl, 'releaseDateMessage', this.validationMessages);
      }));
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  public editGame(): void {
    this.gameService
      .editGame$(this.gameId, this.editGameForm.value)
      .subscribe(() => this.router.navigate(['/']));
  }
}
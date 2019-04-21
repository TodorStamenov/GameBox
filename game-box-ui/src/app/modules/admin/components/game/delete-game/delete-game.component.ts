import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';

import { GameService } from '../../../services/game.service';

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
    private route: ActivatedRoute
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
      .subscribe(res => this.deleteGameForm.setValue({
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

  public deleteGame(): void {
    this.gameService
      .deleteGame$(this.gameId)
      .subscribe(() => this.router.navigate(['/']));
  }
}

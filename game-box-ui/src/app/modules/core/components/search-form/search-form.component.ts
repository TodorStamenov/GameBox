import { FormBuilder, FormGroup } from '@angular/forms';
import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnDestroy,
  ChangeDetectionStrategy
} from '@angular/core';

import { Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SearchFormComponent implements OnInit, OnDestroy {
  private subscription = new Subscription();
  public searchForm: FormGroup;

  @Input() public fieldName: string;
  @Output() public textChange = new EventEmitter<string>();

  constructor(private fb: FormBuilder) { }

  public ngOnInit(): void {
    this.searchForm = this.fb.group({
      [this.fieldName]: [null]
    });

    this.subscription.add(
      this.searchForm.get(this.fieldName).valueChanges
        .pipe(
          debounceTime(500)
        )
        .subscribe(() => this.textChange.emit(this.searchForm.get(this.fieldName).value))
    );
  }

  public ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}

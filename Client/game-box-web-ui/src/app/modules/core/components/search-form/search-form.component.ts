import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  OnDestroy,
  ChangeDetectionStrategy
} from '@angular/core';

import { debounceTime, takeWhile } from 'rxjs/operators';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SearchFormComponent implements OnInit, OnDestroy {
  private componentActive = true;
  public searchForm: UntypedFormGroup;

  @Input() public fieldName: string;
  @Output() public textChange = new EventEmitter<string>();

  constructor(private fb: UntypedFormBuilder) { }

  public ngOnInit(): void {
    this.searchForm = this.fb.group({
      [this.fieldName]: [null]
    });

    this.searchForm.get(this.fieldName).valueChanges.pipe(
      takeWhile(() => this.componentActive),
      debounceTime(500)
    ).subscribe(value => this.textChange.emit(value));
  }

  public ngOnDestroy(): void {
    this.componentActive = false;
  }
}

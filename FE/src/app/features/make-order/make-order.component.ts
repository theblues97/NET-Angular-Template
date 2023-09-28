import { OrderDto } from './../detail-list/detail-list.model';
import { ChangeDetectorRef, Component, OnInit, ChangeDetectionStrategy, ViewChild, TemplateRef, ViewContainerRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormArray, AbstractControl } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { Observable, map } from 'rxjs';
import { MakeOrderService } from './make-order.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { DateAdapter, MatNativeDateModule, MAT_DATE_FORMATS, MAT_DATE_LOCALE } from '@angular/material/core';
import * as moment from 'moment';
import { DATE_FORMAT_STR } from 'src/app/core/utilities/date-utilities';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Status } from 'src/app/core/models/response.model';
import { PopupComponent } from '../popup/popup.component';

@Component({
  selector: 'app-make-order',
  changeDetection: ChangeDetectionStrategy.OnPush,
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    // MatSelectModule,
    // MatFormFieldModule,
    // MatInputModule,
    // MatButtonModule,
    // MatDatepickerModule,
    // MatNativeDateModule
  ],
  providers: [
    MakeOrderService
  ],
  templateUrl: './make-order.component.html',
  styleUrls: ['./make-order.component.scss']
})
export class MakeOrderComponent implements OnInit {
  @ViewChild("template", {read: ViewContainerRef}) template!: TemplateRef<any>;
  @ViewChild("viewContainer", {read: ViewContainerRef}) viewContainer!: ViewContainerRef;
  form: FormGroup;

  constructor(private fb: FormBuilder,
    private makeOrder: MakeOrderService,
    private modalService: NgbModal) {

    this.form = this.fb.group({
      orderList: this.fb.array([])
    })
  }

  ngOnInit(): void {

  }

  get formArray() { return this.form.get('orderList') as FormArray; }

  onAdd() {

    this.formArray.push(this.fb.group(
      {
        customerName: ['', Validators.required],
        customerDob: ['', Validators.required],
        customerEmail: [''],
        shopName: ['', Validators.required],
        shopLocation: [''],
        productName: ['', Validators.required],
        productImage: [''],
        price: ['', Validators.required],
        quantity: ['', Validators.required],
      }
    ));
  }
  
  onRegister() {
    this.formArray.value.map((r: OrderDto) => {
      var dateTime = new Date(r.customerDob);
      r.customerDobStr = moment(dateTime).format(DATE_FORMAT_STR);
    })

    this.makeOrder.register(this.formArray.value).subscribe(rs => {
      if(rs.status != Status.Success) {
        this.alertError(rs.message);
      }
    })
  }

  alertError(msg: string | null) {
		const modalRef = this.modalService.open(PopupComponent, { ariaLabelledBy: 'modal-basic-title' });

    const insComponent = modalRef.componentInstance as PopupComponent
    insComponent.msg = msg ?? "";
	}
}

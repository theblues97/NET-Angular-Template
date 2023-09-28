import { AfterViewInit, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { DetailListService } from './detail-list.service';
import { OrderDto } from './detail-list.model';
import { Status } from 'src/app/core/models/response.model';
import { debounceTime, distinctUntilChanged, fromEvent, tap } from 'rxjs';


@Component({
  selector: 'app-detail-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
  ],
  providers: [DetailListService],
  templateUrl: './detail-list.component.html',
  styleUrls: ['./detail-list.component.scss']
})
export class DetailListComponent implements OnInit, AfterViewInit {

  @ViewChild("search") searchElement!: ElementRef;

  dataSource: OrderDto[] = [];
  displayDataSource: OrderDto[] = [];

  displayedColumns: string[] = [
    'customerName', 
    'customerDobStr', 
    'customerEmail', 
    'shopName', 
    'shopLocation', 
    'productName', 
    'productImage', 
    'price', 
    'quantity'];

  constructor(private detailService: DetailListService) {
    
  }

  ngOnInit(): void {
    this.query();
  }

  ngAfterViewInit(): void {
    fromEvent(this.searchElement.nativeElement, "keyup")
    .pipe(
      debounceTime(1000),
      distinctUntilChanged(),

    ).subscribe(rs => {    
      var filteredData = this.dataSource.filter(x => x.productName.includes(this.searchElement.nativeElement.value))
      this.displayDataSource = [...filteredData];
    });
  }


  query(product?: number) {
    this.detailService.getOrder()
    .subscribe((rs) => {
      if(rs.status == Status.Success) {
        this.dataSource = [... rs.data as OrderDto[]];
        this.displayDataSource = [... this.dataSource];
      }
    })
  }
}

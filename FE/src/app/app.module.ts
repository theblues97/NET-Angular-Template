import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NoopAnimationsModule } from '@angular/platform-browser/animations';
import { DetailListComponent } from './features/detail-list/detail-list.component';
import { HttpClientModule } from '@angular/common/http';
import { ApiService } from './core/api.service';
import { MakeOrderComponent } from './features/make-order/make-order.component';
import { ProductionComponent } from './features/production/production.component';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    ProductionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DetailListComponent,
    MakeOrderComponent,
    HttpClientModule,

    NoopAnimationsModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,

    NgbModule,
  ],
  providers: [ApiService],
  bootstrap: [AppComponent]
})
export class AppModule { }

import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { MatSortModule } from '@angular/material';
import { MatTableModule } from '@angular/material/table';


import { AppComponent } from './app.component';
import { TickerGridComponent } from './ticker-grid/ticker-grid.component';


@NgModule({
  declarations: [
    AppComponent,
    TickerGridComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatSortModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

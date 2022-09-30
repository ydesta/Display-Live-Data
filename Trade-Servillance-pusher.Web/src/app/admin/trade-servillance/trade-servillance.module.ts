import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { TradeServeillanceListComponent } from "./trade-serveillance-list/trade-serveillance-list.component";
import { HttpClientModule } from "@angular/common/http";
import { FlexLayoutModule } from "@angular/flex-layout";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatExpansionModule } from "@angular/material/expansion";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatMenuModule } from "@angular/material/menu";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSelectModule } from "@angular/material/select";
import { MatTableModule } from "@angular/material/table";
import { MatTabsModule } from "@angular/material/tabs";
import { MatToolbarModule } from "@angular/material/toolbar";
import { ToastrModule } from "ngx-toastr";
import { TradeServeillanceRoutingModule } from "./trade-serveillance-routing/trade-serveillance-routing.module";

@NgModule({
  declarations: [TradeServeillanceListComponent],
  imports: [
    CommonModule,
    MatNativeDateModule,
    MatInputModule,
    HttpClientModule,
    FlexLayoutModule,
    MatCardModule,
    MatButtonModule,
    MatExpansionModule,
    MatIconModule,
    MatFormFieldModule,
    MatToolbarModule,
    MatTableModule,
    MatExpansionModule,
    MatIconModule,
    MatDatepickerModule,
    FlexLayoutModule,
    ReactiveFormsModule,
    MatSelectModule,
    MatCardModule,
    MatTabsModule,
    MatCheckboxModule,
    MatMenuModule,
    ToastrModule.forRoot(),
    MatPaginatorModule,
    TradeServeillanceRoutingModule,
  ],
})
export class TradeServillanceModule {}

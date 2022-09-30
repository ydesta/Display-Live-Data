import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { childRoutes } from "../../child-routes";
import { RouterModule, Routes } from "@angular/router";
import { TradeServeillanceListComponent } from "../trade-serveillance-list/trade-serveillance-list.component";

const routes: Routes = [
  {
    path: "",
    component: TradeServeillanceListComponent,
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TradeServeillanceRoutingModule {}

import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule, Routes } from "@angular/router";
import { OrderServeillanceListComponent } from "../order-serveillance-list/order-serveillance-list.component";
import { childRoutes } from "../../child-routes";

const routes: Routes = [
  {
    path: "",
    component: OrderServeillanceListComponent,
  },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class OrderServeillanceRoutingModule {}

import { Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { MatAccordion } from "@angular/material/expansion";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { OrderServeillanceService } from "../service/order-serveillance.service";
import * as signalR from "@microsoft/signalr";
import { environment } from "src/environments/environment";
import { MatTableDataSource } from "@angular/material/table";
import { EcxOrder } from "../model/ecx-order";
@Component({
  selector: "app-order-serveillance-list",
  templateUrl: "./order-serveillance-list.component.html",
  styleUrls: ["./order-serveillance-list.component.scss"],
})
export class OrderServeillanceListComponent implements OnInit {
  @ViewChild(MatAccordion) accordion!: MatAccordion;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  taxDeclareForm!: FormGroup;
  dataSource = new MatTableDataSource<EcxOrder>();
  displayedColumns: string[] = [
    "CommodityType",
    "CommodityClass",
    "CommodityGrade",
    "ProductionYear",
    "OrderType",
    "Warehouse",

    "Member",
    "Price",
    "Quantity",
    "SubmittedTimestamp",
  ];
  constructor(private orderServeillanceService: OrderServeillanceService) {}

  ngOnInit(): void {
    this.getOrderServeillanceList();

    const connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(environment.baseUrl + "notify")
      .build();

    connection
      .start()
      .then(function () {
        console.log("SignalR Connected!");
      })
      .catch(function (err) {
        return console.error(err.toString());
      });

    connection.on("BroadcastMessage", () => {
      console.log("Broad Cast Message");
      this.getOrderServeillanceList();
    });
  }

  getOrderServeillanceList() {
    this.orderServeillanceService
      .getOrderServeillanceList()
      .subscribe((res) => {
        console.log("###       ", res.length);
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      });
  }
}

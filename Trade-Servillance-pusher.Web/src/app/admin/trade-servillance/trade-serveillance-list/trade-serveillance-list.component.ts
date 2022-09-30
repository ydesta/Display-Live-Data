import { Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup } from "@angular/forms";
import { MatAccordion } from "@angular/material/expansion";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import * as signalR from "@microsoft/signalr";
import { environment } from "src/environments/environment";
import { EcxTrade } from "../model/ecx-trade";
import { TradeServeillanceService } from "../service/trade-serveillance.service";

@Component({
  selector: "app-trade-serveillance-list",
  templateUrl: "./trade-serveillance-list.component.html",
  styleUrls: ["./trade-serveillance-list.component.scss"],
})
export class TradeServeillanceListComponent implements OnInit {
  @ViewChild(MatAccordion) accordion!: MatAccordion;

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  taxDeclareForm!: FormGroup;
  dataSource: EcxTrade[] = [];
  displayedColumns: string[] = [
    "TradedTimestamp",
    "BuyerMember",
    "SellerMember",
    "Warehouse",
    "ProductionYear",
    "CommodityType",
    "Symbol",
    "TradeQuantity",
    "TradePrice",
  ];
  constructor(private tradeServeillanceService: TradeServeillanceService) {}

  ngOnInit(): void {
    this.getTradeServeillanceList();
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
      this.getTradeServeillanceList();
    });
  }

  getTradeServeillanceList() {
    this.tradeServeillanceService
      .getTradeServeillanceList()
      .subscribe((res) => {
        console.log("####    ", res);
        this.dataSource = res;
      });
  }
}

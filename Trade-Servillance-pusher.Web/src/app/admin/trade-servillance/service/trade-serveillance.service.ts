import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, throwError, of } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { EcxTrade } from "../model/ecx-trade";
import { applicationApis } from "./applicationApis";

@Injectable({
  providedIn: "root",
})
export class TradeServeillanceService {
  constructor(private http: HttpClient) {}

  getTradeServeillanceList(): Observable<EcxTrade[]> {
    return this.http
      .get<EcxTrade[]>(applicationApis.tradeServeillanceUrl)
      .pipe(catchError(this.handleError));
  }
  private handleError(err) {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}

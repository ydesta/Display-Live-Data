import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable, throwError, of } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { EcxOrder } from "../model/ecx-order";
import { applicationApis } from "../../trade-servillance/service/applicationApis";

@Injectable({
  providedIn: "root",
})
export class OrderServeillanceService {
  constructor(private http: HttpClient) {}

  getOrderServeillanceList(): Observable<EcxOrder[]> {
    return this.http
      .get<EcxOrder[]>(applicationApis.orderServeillanceUrl)
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

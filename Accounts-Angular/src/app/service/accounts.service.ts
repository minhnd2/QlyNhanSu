import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { Accounts } from './model/accounts';
import { ServiceApi } from './serviceApi';
import { AccountSearch } from './model/accountSearch';
import { AccountResult } from './model/accountResult';
import { Login } from './model/login';

@Injectable({
  providedIn: 'root'
})
export class AccountsService {

  private header = new HttpHeaders();
  private serviceApi = new ServiceApi();
  constructor(private http: HttpClient) {
    this.header.append('Content-Type', 'application/json');
    this.header.append('Access-Control-Allow-Origin', '*');
  }

  search(accountSearch: AccountSearch): Observable<AccountResult>{
    return this.http.post<AccountResult>(this.callUrlService(this.serviceApi.accounts.search), accountSearch);
  }

  insert(accounts: Accounts): Observable<Accounts>{
    return this.http.post<Accounts>(this.callUrlService(this.serviceApi.accounts.insert), accounts);
  }

  update(accounts: Accounts): Observable<Accounts>{
    return this.http.post<Accounts>(this.callUrlService(this.serviceApi.accounts.update), accounts);
  }

  delete(accounts: Accounts): Observable<Accounts>{
    return this.http.post<Accounts>(this.callUrlService(this.serviceApi.accounts.delete), accounts);
  }

  login(account: Login): Observable<string>{
    return this.http.post<string>(this.callUrlService(this.serviceApi.accounts.login), account);
  }

  callUrlService(urlService){
    return this.serviceApi.getUrlService(urlService);
  }

}

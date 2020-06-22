export class ServiceApi {
    urlService = "http://localhost:6868/AccountManagerAPI/API";
    accounts = {
        login : "/Accounts/Login", search : "/Accounts/Search", 
        insert : "/Accounts/Insert", update : "/Accounts/Update", 
        delete : "/Accounts/Delete",
    }

    getUrlService(url){
        return this.urlService + url;
    }
}


Vue.mixin({
    data: function () {
        return {
            get serviceAPIs() {
                return {
                    'account': {
                        'get': '/AccountManagerAPI/API/Accounts/Get',
                        'getAll':'/AccountManagerAPI/API/Accounts/GetAll',
                        'search': '/AccountManagerAPI/API/Accounts/Search',
                        'insert': '/AccountManagerAPI/API/Accounts/Insert',
                        'update': '/AccountManagerAPI/API/Accounts/Update',
                        'delete': '/AccountManagerAPI/API/Accounts/Delete',
                        'login': '/AccountManagerAPI/API/Accounts/Login',
                    },                    
                }
            },
        }
    }
});

Vue.filter('formatDate', function (value) {
    if (value) {
        return moment(String(value)).format('DD/MM/YYYY hh:mm')
    }
});
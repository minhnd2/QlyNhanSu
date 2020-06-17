
var app = new Vue({
    el: '#app',
    data() {
        return {
            disable: false,
            msgAlert: '',
            username: '',
            pass: '',
            templateItem: {
                Mail: null,
                PassWord: null,
            },
        }
    },
    methods: {
        login() {
            var _this = this;
            if (!this.templateItem.Mail) {
                return null;
            }
            if (!this.templateItem.PassWord) {
                return null;
            }
            axios.post(this.serviceAPIs.account.login, this.templateItem)
                .then(
                    response => {
                        //update  list
                        _this.disable = false;
                        _this.templateItem.PassWord = null;
                        window.location = "/pages/account/list.html";
                    })
                .catch(error => {
                    _this.disable = true;
                    _this.msgAlert = error.data.Message;
                    _this.templateItem.PassWord = null;
                });
        },
    },
})
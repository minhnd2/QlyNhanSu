var items = [];

var app = new Vue({
    el: '#app',
    data() {
        return {
            tableValues: [],
            checkedItems: [],
            selectedAll: false,
            items: items,
            currentPage: 1,
            perPage: 5,
            totalRows: items.length,
            fields: [
                { key: 'no', label: 'STT', checked: true },
                { key: 'Name', label: 'Tên', checked: true, class: 'desc' },
                { key: 'Mail', label: 'Mail', checked: true, class: 'desc' },
                { key: 'Phone', label: 'Số điện thoại', checked: true, class: 'desc' },
                { key: 'menu-action', label: '', checked: true },

            ],
            templateItem: {
                ID: null,
                Name: null,
                Mail: null,
                Phone: null,
                PassWord: null,
            },
            templateSearch: {
                id: null,
                Name: "",
                Mail: "",
                Phone: "",
                status: 1,
                createDate: null,
                pageIndex: 1,
                pageSize: 1,
                currentPage: 1,
            },
            isAddNew: false,
            inputNamme: false,
            inputMail: false,
            inputPass: false,
            inputPhone: false,
        }
    },
    created() {
        getList(this);
    },
    watch: {
        currentPage(a, b) {
            if (a !== b) {
                getList(this);
            }
        },
    },
    methods: {
        edit(item, index, button) {
            this.templateItem.ID = item.ID;
            this.templateItem.Name = item.Name;
            this.templateItem.Mail = item.Mail;
            this.templateItem.Phone = item.Phone;
            //this.templateItem.CreateDate = item.CreateDate;
            this.templateItem.CreateDate = moment(item.CreateDate).format('DD/MM/YYYY hh:mm');

            this.templateItem.IsStatus = item.IsStatus;

            this.isAddNew = false;
        },
        add() {
            this.isAddNew = true;
            this.templateItem.ID = null;
            this.templateItem.Name = null;
            this.templateItem.Mail = null;
            this.templateItem.Phone = null;
            this.templateItem.PassWord = null;
            this.templateItem.IsStatus = true;
            this.templateItem.CreateDate = moment().format('DD/MM/YYYY hh:mm');

            this.inputNamme = false;
            this.inputMail = false;
            this.inputPass = false;
            this.inputPhone = false;
        },
        save() {
            var _this = this;
            if (!this.templateItem.Name) {
                return null;
            }
            if (!this.templateItem.Mail) {
                return null;
            }
            if (!this.templateItem.Phone) {
                return null;
            }
            if (!/^\d{4} \d{3} \d{3}$/.test(this.templateItem.Phone)) {
                return null;
            }
            if (!/[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$/.test(this.templateItem.Mail)){
                return null;
            }
            
            if (this.isAddNew) {
                this.templateItem.CreateDate = moment().format('DD/MM/YYYY hh:mm');
            }

            $('#modalCreate').modal('hide');
            $('#modalConfirmSave').modal('show');
            return false;
        },
        search() {
            getList(this);
        },
        confirmSave() {
            var _this = this;
            if (this.isAddNew) {
                this.templateItem.CreateDate = null;
                axios.post(this.serviceAPIs.account.insert, this.templateItem)
                    .then(
                        response => {
                            //update  list
                            _this.currentPage = 1;
                            getList(_this);
                            $('#modalConfirmSave').modal('hide');
                        })
                    .catch(error => {
                        console.log("error");
                    });
            }
            else {
                axios.post(this.serviceAPIs.account.update, this.templateItem)
                    .then(
                        response => {
                            //update  list
                            getList(_this);
                            $('#modalConfirmSave').modal('hide');
                        })
                    .catch(error => {
                        console.log("error");
                    });
            }
        },
        deleteAccount() {
            $("#modalViewDelete").modal('hide');
            $('#modalConfirmDelete').modal('show');
        },
        confirmDel() {
            var _this = this;
            axios.post(this.serviceAPIs.account.delete, this.templateItem)
                .then(
                    response => {
                        //update  list
                        getList(_this);
                        $('#modalConfirmDelete').modal('hide');
                    })
                .catch(error => {
                    console.log("error");
                });
        },
    },
})

function getList(_this) {
    _this.templateSearch.pageIndex = _this.currentPage;
    _this.templateSearch.pageSize = _this.perPage;
    axios.post(_this.serviceAPIs.account.search, _this.templateSearch)
        .then(
            response => {
                //update  list
                _this.items = response.data.ListAccount;                
                _this.totalRows = response.data.TotalRow;
            })
        .catch(error => {

        });
}

$(document).ready(function () {

    $("#hf-phone").inputmask({
        mask: '9999 999 999',
        showMaskOnHover: false,
        showMaskOnFocus: false,
        onBeforePaste: function (pastedValue, opts) {
            var processedValue = pastedValue;

            //do something with it

            return processedValue;
        }
    });
});
var app = new Vue({
    el: '#app',

    data: {

        status: 0,
        loading: false,
        orders: [],
        selectedOrder: null

    },

    mounted() {
        this.getOrders();
              
    },
    watch: {
        status: function () {
            this.getOrders();
        }
    },
    methods: {
        getOrders() {
            this.loading = true;
            axios.get('/orders?status=' + this.status)
                .then(res => {
                    this.orders = res.data;
                    this.loading = false;

                });
        },
         selectOrder(id) {
            this.loading = true;
            axios.get('/orders/' + id)
                .then(res => {
                    this.selectedOrder = res.data;
                    this.loading = false;

                });
        },
        updateOrder() {
            this.loading = true;
            axios.put('/orders/' + this.selectedOrder.id, null)
                .then(res => {
                    this.loading = false;
                    this.exitOrder();
                    this.getOrders();

                });
        },
        exitOrder() {
            this.selectedOrder = null;
        }



    },
    computed: {

    }


});
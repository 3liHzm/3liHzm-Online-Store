var app = new Vue({
    el: '#app',

    data: {

        selectedProduct: null,
      //  loading: false,
        products: [],
        newStock: {
            productId: 0,
            description: "Stok",
            qty: 10
        }

    },

    mounted() {
        this.getStocks();

    },
    methods: {
        getStocks() {
            this.loading = true;
            axios.get('/stocks/')
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },

        selectProduct(product) {

            this.selectedProduct = product;
            this.newStock.productId = product.id;

        },
        UpdateStock() {
            axios.put('/stocks/', {

               stock: this.selectedProduct.stock.map(x => {
                    return {
                        id: x.id,
                        description: x.description,
                        qty: x.qty,
                        productId: this.selectedProduct.id
                    };
            
                })
            })
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.splice(index, 1)
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },

        deleteStock(id, index) {
            axios.delete('/stocks/' + id)
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.splice(index, 1)
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });

        },
        addStock() {
            this.loading = true;
            axios.post('/stocks/', this.newStock)
                .then(res => {
                    console.log(res);
                    this.selectedProduct.stock.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },


    }


});
var app = new Vue({
    el: '#app',
    data: {

        term:"",
        file: '',
            editing: false,
            objectIndex: 0,
            loading: false,
            productModel: {
                id: 0,
                Name: "porductName",
                Description: "desc",
                Value: 0.99,
                CatagoryId: 0,
               
               // File: ''
        },
        categories: [],
        products: [],
        newStock: {
            productId: 0,
            description: "Stok",
            qty: 10
        },
          
        selectedProduct: null,


    },

    mounted() {
     
       // this.getStocks(); 
        this.getProducts();
        


    },
    methods: {

        serchProduct() {     
            axios.post('/products/search/' + this.term)
                .then(res => {
                    console.log(res);
                    this.products = res.data;
                })
                .catch(err => {
                    console.log(err);
                    this.getProducts();
                })
                .then(() => {
                    this.loading = false;
                });
           
        },

        getCategories() {
            this.loading = true;
            axios.get('/Catagories/')
                .then(res => {
                    console.log(res);
                    this.categories = res.data;
                })
                .catch(err => {
                    console.log(err);
                }).then(() => {
                    this.loading = false;
                });
        },

        getProduct(id) {
            this.loading = true;
            axios.get('/products/' + id)
                .then(res => {
                    console.log(res);
                    var product = res.data;
                    this.productModel = {
                        id: product.id,
                        Name: product.name,
                        Description: product.description,
                        Value: product.value,
                        
                        //ImgUrl: product.ImgUrl
                        
                    }
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },





        getProducts() {
            this.loading = true;
            axios.get('/products/')
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
        handleFileUpload() {
            this.file = this.$refs.file.files[0];;
        },
        postProdutc() {
           

            let formData = new FormData();

            formData.append('id', this.productModel.id);
            formData.append('CatagoryId', this.productModel.CatagoryId);
            formData.append('Name', this.productModel.Name);
            formData.append('Description', this.productModel.Description);
            formData.append('Value', this.productModel.Value);
            formData.append('File', this.file);
         
            this.loading = true;

            axios.post('/products/', formData /*this.productModel*/)
                .then(res => {
                    console.log(res.data);
                    this.products.push(res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                    this.getProducts();

                });
        },

        updateProdutc() {

            let formData = new FormData();

            formData.append('id', this.productModel.id);
            formData.append('CatagoryId', this.productModel.CatagoryId);
            formData.append('Name', this.productModel.Name);
            formData.append('Description', this.productModel.Description);
            formData.append('Value', this.productModel.Value);
            formData.append('File', this.file);

            this.loading = true;
            axios.put('/products/', formData/*this.productModel*/)
                .then(res => {
                    console.log(res.data);
                    this.products.splice(this.objectIndex, 1, res.data);
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                    this.editing = false;
                    //this.getStocks();
                });
        },
        newProduct() {
            this.productModel.id = 0;
            this.editing = true;

        },
        editProduct(id, index) {
            this.objectIndex = index;
            this.getProduct(id);
            this.getCategories();
            this.editing = true;

        },

        cancel() {
            this.editing = false;
            this.selectedProduct = null;
            
        },


        deleteProduct(id, index) {
            this.loading = true;
            axios.delete('/products/' + id)
                .then(res => {
                    console.log(res);
                    this.products.splice(index, 1)
                })
                .catch(err => {
                    console.log(err);
                })
                .then(() => {
                    this.loading = false;
                });
        },



        //stocks

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






    },

    computed: {

    }

});                       
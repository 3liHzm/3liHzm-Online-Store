var app = new Vue({
    el: '#app',

    data: {

        selectedCatagory: null,
        categories:[],
        newCatagory: {
            id: 0,
            catagory: "",

        }

    },
    mounted() {
        this.getCategories();
    },

    methods: {
        addCatagory() {
            axios.post('/Catagories/', this.newCatagory)
                .then(res => {
                    console.log(res);
                    this.categories.push(res.data)
                })
                .catch(err => {
                    console.log(err);
                });

        },

        getCategories() {
            axios.get('/Catagories/')
                .then(res => {
                    console.log(res);
                    this.categories = res.data;
                })
                .catch(err => {
                    console.log(err);
                })

        },


        selectCatagory(catagory) {

            this.selectedCatagory = catagory;
            this.newCatagory.id = catagory.id;
        },

        deleteCatagory(id) {
            axios.delete('/Catagories/' + id)
                .then(res => {
                    console.log(res);
                    this.getCategories();
                    this.selectedCatagory = null;
                })
                .catch(err => {
                    console.log(err);
                });

        },


        UpdateCatagory() {
            axios.put('/Catagories/', this.selectedCatagory)
                .then(res => {
                    console.log(res);
                    this.getCategories();
                    this.selectedCatagory = null;
                })
                .catch(err => {
                    console.log(err);
                });
        },

    },



});
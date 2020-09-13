import { createApp } from 'vue'
import App from './App.vue'
import axios from 'axios'
import VueAxios from 'vue-axios'

// App.use(VueAxios, axios)

createApp(App)
    .mount('#app')
    .use(VueAxios, axios)

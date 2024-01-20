import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import PrimeVue from "primevue/config";
import InputText from "primevue/inputtext";
import Dropdown from "primevue/dropdown";
import InputNumber from "primevue/inputnumber";
import ProgressBar from "primevue/progressbar";
import InputSwitch from "primevue/inputswitch";
import prototypes from "@/prototypes";
import Button from "primevue/button";
import Navigation from "@/components/Navigation.vue";
import {TinyEmitter} from "tiny-emitter";
prototypes()
const app = createApp(App);
app.provide('emitter', new TinyEmitter())
app.use(PrimeVue);
app.component('InputText', InputText)
app.component('InputNumber', InputNumber)
app.component('ProgressBar', ProgressBar)
app.component('DropDown', Dropdown)
app.component('InputSwitch', InputSwitch)
app.component('Button', Button)
app.component('Navigation', Navigation)
app.use(router)
app.mount('#app')

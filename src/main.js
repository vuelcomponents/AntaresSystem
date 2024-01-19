import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import PrimeVue from "primevue/config";
import Menubar from "primevue/menubar";
import Message from "primevue/message";
import {AgGridVue} from "ag-grid-vue3";
import InputText from "primevue/inputtext";
import Calendar from "primevue/calendar";
import {TinyEmitter} from "tiny-emitter";
import Breadcrumb from "primevue/breadcrumb";
import { createVuetify } from 'vuetify'
import {mdi} from "vuetify/iconsets/mdi";
import {aliases} from "vuetify/iconsets/fa";
import {md} from "vuetify/iconsets/md";
import {VStepper, VStepperHeader, VStepperItem} from "vuetify/labs/components";
import InputNumber from "primevue/inputnumber";
import ProgressSpinner from "primevue/progressspinner";
import InputClick from "@/components/InputClick.vue";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import SelectionManager from "@/components/SelectionManager/SelectionManager.vue";
import FileUpload from "primevue/fileupload";
import DataPopup from "@/components/popups/DataPopup.vue";
import CollectionRenderer from "@/components/CollectionManager/CollectionRenderer.vue";
import SelectionRenderer from "@/utils/grid/SelectionRenderer.vue";
import ColorPicker from "primevue/colorpicker";
import ColorPickerRenderer from "@/utils/grid/ColorPickerRenderer.vue";
import StatusSwitchRenderer from "@/utils/grid/StatusSwitchRenderer.vue";
import DateRenderer from "@/utils/grid/DateRenderer.vue";
import ParsingCalendar from "@/components/ParsingCalendar/ParsingCalendar.vue";
import Editor from "primevue/editor";
import AdviseRowRenderer from "@/views/dashboard/DataView/companies/root/AdviseRowRenderer.vue";
import ProgressBar from "primevue/progressbar";
import InputSwitch from "primevue/inputswitch";
import SharkStepper from "@/components/SharkStepper.vue";
import Dropdown from "primevue/dropdown";


const app = createApp(App);

app.use(PrimeVue);
app.component('MenuBar', Menubar)
app.component('MesSage', Message)
app.component('InputText', InputText)
app.component('InputNumber', InputNumber)
app.component('AgGridVue', AgGridVue)
app.component('Calendar', Calendar)
app.component('Breadcrumb', Breadcrumb)
app.component('Spinner', ProgressSpinner)
app.component('InputClick', InputClick)
app.component('FileUpload', FileUpload)
app.component('CollectionManager', CollectionManager)
app.component('SelectionManager', SelectionManager)
app.component('DataPopup', DataPopup)
app.component('CollectionRenderer', CollectionRenderer)
app.component('SelectionRenderer', SelectionRenderer)
app.component('ColorPicker', ColorPicker)
app.component('ColorPickerRenderer', ColorPickerRenderer)
app.component('StatusSwitchRenderer', StatusSwitchRenderer)
app.component('DateRenderer', DateRenderer)
app.component('ParsingCalendar', ParsingCalendar)
app.component('Editor', Editor)
app.component('AdviseRowRenderer', AdviseRowRenderer)
app.component('ProgressBar', ProgressBar)
app.component('DropDown', Dropdown)
app.component('InputSwitch', InputSwitch)
app.component('SharkStepper', SharkStepper)
app.provide('emitter', new TinyEmitter())


app.component('InputNumber', {
    extends: InputNumber,
    props: {
        maxFractionDigits: {
            type: Number,
            default: 2
        },
        minFractionDigits: {
            type: Number,
            default: 2
        },
        locale: {
            type: String,
            default: 'pl-PL'
        }
    }
});

const vuetify = createVuetify({
    icons: {
        defaultSet: 'mdi',
        aliases,
        sets: {
            mdi,
            md
        },
    },
    components:{
        VStepper,
        VStepperHeader,
        VStepperItem,

    },
})

app.use(vuetify);
app.use(store);
app.use(router);
app.mount('#app')

<template>
  <div style="display:grid; grid-template-columns: 1fr 1fr">
      <MenuBar class="on-dark-bg flex-left" :model="menu" style="border-radius:0; border:none;"/>
      <div class="search_inp">
      <span class="p-input-icon-left" style="display:flex;">
            <i class="pi pi-search" v-if="grid.searchText===''" style="margin-left:60px;margin-top:-7px"/>
          <InputText v-model="grid.searchText" @input="grid.search()" placeholder="Search" style="width:40vw; border-radius:0"/>
      </span>
      </div>
  </div>
  <router-view></router-view>
  <ag-grid-vue
          class="ag-theme-alpine _grid grid-black"
          style="height: 500px;width: 100%"
          :rowData="grid.data"
          :grid-options="grid.options"
          rowSelection="multiple"
          animateRows="true"
          :columnDefs="getColumns()"
  >
  </ag-grid-vue>
  <div class="grid-line"></div>
  <div class="grid-line" style="background:#001c20"></div>
  <div v-if="this.life.id && this.life.param">
    <CollectionRenderer
        owner-key="car"
        :owner-id="this.life.id"
        :param="this.life.param"
        :toast="toast"
        :services="services"
        :crud="crud"
        :emitter="emitter"
        :xpopupOptions="EditOptions"
        :set-collection="setRowCollection"></CollectionRenderer>
  </div>
</template>

<script>

import {getCurrentInstance, inject, onMounted, ref} from "vue";
import {CarService} from "@/services/CarService";
import Toast from "@/utils/classes/Toast";
import EditOptions from "@/views/dashboard/DataView/cars/options/EditOptions";
import CarCrudController from "@/views/dashboard/DataView/cars/controllers/CarCrudController";
import CarMenu from "@/views/dashboard/DataView/cars/CarMenu";
import CarLife from "@/views/dashboard/DataView/cars/CarLife";
import GridClass from "@/utils/grid/GridClass";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";
import onCellClicked from "@/views/dashboard/DataView/cars/functions/onCellClicked";
import CarColumns from "@/views/dashboard/DataView/cars/CarColumns";
import setRowCollection from "@/utils/grid/setRowCollection";

const componentRef = ref(null);
function handleClick(params, del) {
    if(componentRef.value)
        componentRef.value.onCellValueChanged(params, del);
}

export default {
    name: "CarList",
  computed: {
    EditOptions() {
      return EditOptions
    }
  },
    components: {},
    setup(){
        onMounted(()=>{
            componentRef.value = getCurrentInstance().proxy;
        });
        const emitter = inject('emitter')
        const services = {
          com:new CarService(emitter)
        };
        const toast = new Toast()
        const crud = new CarCrudController(services.com,toast)
        return {
          services,
          crud,
          emitter,
          toast
        }
    },

    mounted(){
      this.emitter.on('quickCarAdd',(car)=>{
        this.grid.api.applyTransaction({add:[car]})
      })
    },
    data(){
        return{
            menu:CarMenu(this.createCar,
                              this.cancelChanges,
                              this.deleteSelected,
                              this.saveChanges,
                              this.loadList,
                              this.quickAdd),
            life:new CarLife(),
            grid:new GridClass({
                  onCellValueChanged: null,
                  onCellClicked: this.onCellClicked,
                  onSelectionChanged: this.onSelectionChanged,
                },{
                  loadList:this.loadList
                },
                this.services.com

            ),
            message:null,

        }
    },
    methods:{
      async loadList(){
        const list = await this.crud.getAll();
        this.grid.api.setRowData(list.data)
      },
      closeCreate(){
        this.$router.back()
      },
      onSelectionChanged(params){
        //
        console.log(this.grid.getAllRows())
      },
      deleteSelected(){
        this.crud.deleteMultiple(this.grid.selected).then(res=>{
          if(res?.status === 200){
            this.grid.deleteSelectedRows()
          }
        })
      },
      onCellClicked(params){
        mainOnCellClicked(params, {router:this.$router, life:this.life})
        onCellClicked(params, {router:this.$router})
      },
      onCellValueChanged(params){
        //
      },
      createCar(){
         this.$router.push({name:'CarCreate'})
      },
      cancelChanges(){
         console.log('cancel changes')
      },
      async saveChanges(){
          console.log('save changes')
      },
      getColumns(){
        return CarColumns()
      },
      setRowCollection(collection, key){
        setRowCollection(collection, key, {grid:this.grid, life:this.life})
      },
      quickAdd(){
        this.$route.name === 'CarList'
            ?
            this.$router.push({name: 'CarQuickCreate'})
            :
            this.$router.push({name: 'CarList'})
      }
    }
}
</script>
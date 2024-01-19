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
        owner-key="offer"
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
import {OfferService} from "@/services/OfferService";
import Toast from "@/utils/classes/Toast";
import EditOptions from "@/views/dashboard/DataView/offers/options/EditOptions";
import OfferCrudController from "@/views/dashboard/DataView/offers/controllers/OfferCrudController";
import OfferMenu from "@/views/dashboard/DataView/offers/OfferMenu";
import OfferLife from "@/views/dashboard/DataView/offers/OfferLife";
import GridClass from "@/utils/grid/GridClass";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";
import onCellClicked from "@/views/dashboard/DataView/offers/functions/onCellClicked";
import OfferColumns from "@/views/dashboard/DataView/offers/OfferColumns";
import setRowCollection from "@/utils/grid/setRowCollection";

const componentRef = ref(null);
function handleClick(params, del) {
    if(componentRef.value)
        componentRef.value.onCellValueChanged(params, del);
}

export default {
    name: "OfferList",
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
          com:new OfferService(emitter)
        };
        const toast = new Toast()
        const crud = new OfferCrudController(services.com,toast)
        return {
          services,
          crud,
          emitter,
          toast
        }
    },

    mounted(){
      this.emitter.on('quickOfferAdd',(offer)=>{
        this.grid.api.applyTransaction({add:[offer]})
      })
    },
    data(){
        return{
            menu:OfferMenu(this.createOffer,
                              this.cancelChanges,
                              this.deleteSelected,
                              this.saveChanges,
                              this.loadList,
                              this.quickAdd),
            life:new OfferLife(),
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
      createOffer(){
         this.$router.push({name:'OfferCreate'})
      },
      cancelChanges(){
         console.log('cancel changes')
      },
      async saveChanges(){
          console.log('save changes')
      },
      getColumns(){
        return OfferColumns()
      },
      setRowCollection(collection, key){
        setRowCollection(collection, key, {grid:this.grid, life:this.life})
      },
      quickAdd(){
        this.$route.name === 'OfferList'
            ?
            this.$router.push({name: 'OfferQuickCreate'})
            :
            this.$router.push({name: 'OfferList'})
      }
    }
}
</script>
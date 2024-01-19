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
          style="width: 100%"
          domLayout="autoHeight"
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
        owner-key="document"
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
import CompanyColumns from "@/views/dashboard/DataView/companies/CompanyColumns";
import CompanyMenu from "@/views/dashboard/DataView/companies/CompanyMenu";
import CompanyLife from "@/views/dashboard/DataView/companies/CompanyLife";
import {CompanyService} from "@/services/CompanyService";
import CompanyCrudController from "@/views/dashboard/DataView/companies/controllers/CompanyCrudController";
import onCellClicked from "@/views/dashboard/DataView/companies/functions/onCellClicked";
import onCellValueChanged from "@/views/dashboard/DataView/companies/functions/onCellValueChanged";
import Toast from "@/utils/classes/Toast";
import GridClass from "@/utils/grid/GridClass";
import setRowCollection from "@/utils/grid/setRowCollection";
import CollectionRenderer from "@/components/CollectionManager/CollectionRenderer.vue";
import EditOptions from "@/views/dashboard/DataView/companies/options/EditOptions";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";

const componentRef = ref(null);
function handleClick(params, del) {
    if(componentRef.value)
        componentRef.value.onCellValueChanged(params, del);
}

export default {
    name: "CompanyList",
  computed: {
    EditOptions() {
      return EditOptions
    }
  },
    components: {CollectionRenderer},
    setup(){
        onMounted(()=>{
            componentRef.value = getCurrentInstance().proxy;
        });
        const emitter = inject('emitter')
        const services = {
          com:new CompanyService(emitter)
        };
        const toast = new Toast()
        const crud = new CompanyCrudController(services.com,toast)
        return {
          services,
          crud,
          emitter,
          toast
        }
    },

    mounted(){
      this.emitter.on('quickCompanyAdd',(company)=>{
        this.grid.api.applyTransaction({add:[company]})
      })
    },
    data(){
        return{
            menu:CompanyMenu(this.createCompany,
                              this.cancelChanges,
                              this.deleteSelected,
                              this.saveChanges,
                              this.loadList,
                              this.quickAdd),
            life:new CompanyLife(),
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
      createCompany(){
         this.$router.push({name:'CompanyCreate'})
      },
      cancelChanges(){
         console.log('cancel changes')
      },
      async saveChanges(){
          console.log('save changes')
      },
      getColumns(){
        return CompanyColumns()
      },
      setRowCollection(collection, key){
        setRowCollection(collection, key, {grid:this.grid, life:this.life})
      },
      quickAdd(){
        this.$route.name === 'CompanyList'
            ?
            this.$router.push({name: 'CompanyQuickCreate'})
            :
            this.$router.push({name: 'CompanyList'})
      }
    }
}
</script>
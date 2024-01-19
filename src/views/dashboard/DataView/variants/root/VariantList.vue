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
            :rowData="grid.data"
            domLayout="autoHeight"
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
          owner-key="variant"
          :owner-id="this.life.id"
          :param="this.life.param"
          :toast="toast"
          :services="services"
          :crud="crud"
          :emitter="emitter"
          :xpopupOptions="VariantEditOptions"
          :set-collection="this.setRowCollection"></CollectionRenderer>
    </div>
</template>

<script>
import {getCurrentInstance, inject, onMounted, ref} from "vue";

import GridClass from "@/utils/grid/GridClass";
import VariantColumns from "@/views/dashboard/DataView/variants/VariantColumns";
import VariantCrudController from "@/views/dashboard/DataView/variants/controllers/VariantCrudController";
import VariantMenu from "@/views/dashboard/DataView/variants/VariantMenu";
import VariantLife from "@/views/dashboard/DataView/variants/VariantLife";
import variantOnCellClicked from "@/views/dashboard/DataView/variants/functions/variantOnCellClicked";
import variantOnCellValueChanged from "@/views/dashboard/DataView/variants/functions/variantOnCellValueChanged";
import {VariantService} from "@/services/VariantService";
import Toast from "@/utils/classes/Toast";
import CollectionRenderer from "@/components/CollectionManager/CollectionRenderer.vue";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";
import VariantEditOptions from "@/views/dashboard/DataView/variants/options/VariantEditOptions";
import setRowCollection from "@/utils/grid/setRowCollection";

const componentRef = ref(null);
function handleClick(params, del) {
    if(componentRef.value)
     componentRef.value.onCellValueChanged(params, del);
}

export default {
    name: "VariantList",
  computed: {
    VariantEditOptions() {
      return VariantEditOptions
    }
  },
    components: {CollectionRenderer},
    setup(){
        onMounted(()=> { componentRef.value = getCurrentInstance().proxy; });
        const emitter = inject('emitter')
        const toast =new Toast();
        const services = {
          variant: new VariantService(emitter)
        };
        const crud = new VariantCrudController(services.variant)
        return {
          services,
          crud,
          toast,
          emitter
        }
    },
    mounted(){
      this.emitter.on('quickVariantAdd',(variant)=>{
        this.grid.api.applyTransaction({add:[variant]})
      })
    },
    data(){
        return{
            menu:VariantMenu(this.createVariant,
                            this.cancelChanges,
                            this.deleteSelected,
                            this.saveChanges,
                            this.loadList,
                            this.quickAdd),
            life:new VariantLife(),
            grid:new GridClass({
                onCellValueChanged: null,
                onCellClicked: this.onCellClicked,
                onSelectionChanged: this.onSelectionChanged,
              },{
                loadList:this.loadList
              },
                this.services.variant
            ),
            message:null,

        }
    },
    methods:{
      setRowCollection(collection, key){
        setRowCollection(collection, key, {grid:this.grid, life:this.life})
      },
        async loadList(){
          const list = await this.crud.getAll()
          this.grid.api.setRowData(list.data)
        },
        onCellClicked(params){
          variantOnCellClicked(params, {router:this.$router, life:this.life})
        },

        onCellValueChanged(params){
            //
        },
        onSelectionChanged(params){
          //
        },
        createVariant(){
          this.$router.push({name:'VariantCreate'})
        },
        deleteSelected(){
          this.crud.deleteMultiple(this.grid.selected).then(res=>{
            if(res?.status === 200){
              this.grid.deleteSelectedRows()
            }
          })
        },
        cancelChanges(){
          console.log('cancel changes')
        },
        async saveChanges(){
            await this.crud.saveChanges()
        },
        getColumns(){
          return VariantColumns()
        },
        quickAdd(){
          this.$route.name === 'VariantList'
              ?
              this.$router.push({name: 'VariantQuickCreate'})
              :
              this.$router.push({name: 'VariantList'})
        }
    }
}
</script>


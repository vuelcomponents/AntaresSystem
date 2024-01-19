<template>
   <div style="display:grid; grid-template-columns: 1fr 1fr">
        <MenuBar class="on-dark-bg flex-left" :model="menu" style="border-radius:0; border:none;"/>
        <div class="search_inp">
        <div class="p-input-icon-left" style="display:flex;">
            <i class="pi pi-search" v-if="grid.searchText===''" style="margin-left:60px;margin-top:-7px"/>
            <InputText v-model="grid.searchText" @input="grid.search()" placeholder="Search" style="width:40vw; border-radius:0"/>
        </div>
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
          owner-key="actionFunction"
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
import EditOptions from "@/views/dashboard/DataView/action/options/EditOptions";
import Toast from "@/utils/classes/Toast";
import ActionFunctionCrudController from "@/views/dashboard/DataView/action/controllers/ActionFunctionCrudController";
import ActionFunctionMenu from "@/views/dashboard/DataView/action/ActionFunctionMenu";
import ActionFunctionLife from "@/views/dashboard/DataView/action/ActionFunctionLife";
import GridClass from "@/utils/grid/GridClass";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";
import onCellClicked from "@/views/dashboard/DataView/action/functions/onCellClicked";
import CollectionRenderer from "@/components/CollectionManager/CollectionRenderer.vue";
import {ActionFunctionService} from "@/services/ActionFunctionService";
import setRowCollection from "@/utils/grid/setRowCollection";
import ActionFunctionColumns from "@/views/dashboard/DataView/action/ActionFunctionColumns";


const componentRef = ref(null);
function handleClick(params, del) {
    if(componentRef.value)
     componentRef.value.onCellValueChanged(params, del);
}

export default {
    name: "ActionFunctionList",
  computed: {
    EditOptions() {
      return EditOptions
    }
  },
    components: {CollectionRenderer},
    setup(){
        onMounted(()=> { componentRef.value = getCurrentInstance().proxy; });
        const emitter = inject('emitter')
        const toast =new Toast();
        const services = {
          actionFunction: new ActionFunctionService(emitter)
        };
        const crud = new ActionFunctionCrudController(services.actionFunction)
        return {
          services,
          crud,
          emitter,
          toast
        }
    },
    mounted(){
      this.emitter.on('quickActionFunctionAdd',(actionFunction)=>{
        this.grid.api.applyTransaction({add:[actionFunction]})
      })
    },
    data(){
        return{
            menu:ActionFunctionMenu(this.createActionFunction,
                              this.cancelChanges,
                              this.deleteSelected,
                              this.saveChanges,
                              this.loadList,
                              this.quickAdd),
            life:new ActionFunctionLife(),
            grid:new GridClass({
                onCellValueChanged: null,
                onCellClicked: this.onCellClicked,
                onSelectionChanged: this.onSelectionChanged,
              },{
                loadList:this.loadList
              },
              this.services.actionFunction
            ),
            message:null,

        }
    },
    methods:{
        async loadList(){
          const list = await this.crud.getAll()
          this.grid.api.setRowData(list.data)
        },
        onCellClicked(params){
          mainOnCellClicked(params, {router:this.$router, life:this.life})
          onCellClicked(params, {router:this.$router, life:this.life})
        },

        onCellValueChanged(params){
            //
        },
        onSelectionChanged(params){
          //
        },
        createActionFunction(){
          this.$router.push({name:'ActionFunctionCreate'})
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
          return ActionFunctionColumns()
        },
        setRowCollection(collection, key){
          setRowCollection(collection, key, {grid:this.grid, life:this.life})
        },
        quickAdd(){
          this.$route.name === 'ActionFunctionList'
              ?
              this.$router.push({name: 'ActionFunctionQuickCreate'})
              :
              this.$router.push({name: 'ActionFunctionList'})
        }
    }
}
</script>

<style>
.grid-line{
    height:5px;
    width:100%;
    background:#01252a;
}
.s{
    display:none!important;
}
.background-red{
    background: #cc8925 !important;
    margin-inline:2px!important;
}
.custom-active-cell{
    transition: all 0.2s, background-color 1s ease;
    border:solid 1px #00000005!important;
}
@keyframes blankRed{
    0%{
        background:#cc8925!important;
    }
    100%{
        background:white;
    }
}
select{
    border:none;
    background:#fDfDfD;
    border-radius:2px;
}
.button-collection{
    border:none;
    background:#fDfDfD;
    margin-left:3px;
    border-radius:2px;
    cursor:pointer;
}
.biggerFont{
    font-size:1.5em!important;
}
.flexCenter{
    display:flex;
    justify-content:center;
    align-items:center;
}
.prompts-loops{
    font-size:0.8em;
    padding:3px;
    background: rgba(255, 255, 255, 0.1);
    margin-block:1px;
    display:grid;
    grid-template-columns:1fr 1fr;
}
.column-manager{
    position:absolute;
    top:0;
    left:200px;
    z-index:3;
}

</style>
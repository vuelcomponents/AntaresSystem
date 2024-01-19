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
            :rowData="grid.data"
            :grid-options="grid.options"
            rowSelection="multiple"
            animateRows="true"
            domLayout="autoHeight"
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
import GridClass from "@/utils/grid/GridClass";
import {DocumentService} from "@/services/DocumentService";
import DocumentMenu from "@/views/dashboard/DataView/documents/DocumentMenu";
import DocumentLife from "@/views/dashboard/DataView/documents/DocumentLife";
import DocumentCrudController from "@/views/dashboard/DataView/documents/controllers/DocumentCrudController";
import onCellClicked from "@/views/dashboard/DataView/documents/functions/onCellClicked";
import DocumentColumn from "@/views/dashboard/DataView/DocumentColumn";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";
import setRowCollection from "@/utils/grid/setRowCollection";
import Toast from "@/utils/classes/Toast";
import CollectionRenderer from "@/components/CollectionManager/CollectionRenderer.vue";
import EditOptions from "@/views/dashboard/DataView/documents/options/EditOptions";
const componentRef = ref(null);
function handleClick(params, del) {
    if(componentRef.value)
     componentRef.value.onCellValueChanged(params, del);
}

export default {
    name: "DocumentList",
  computed: {
    EditOptions() {
      return EditOptions
    }
  },
    components: {CollectionRenderer},
    setup(){
        onMounted(()=> { componentRef.value = getCurrentInstance().proxy; });
        const emitter = inject('emitter')
        const toast = new Toast()
        const services = {
          document: new DocumentService(emitter),
        };
        const crud = new DocumentCrudController(services.document)
        return {
          services,
          crud,
          toast,
          emitter
        }
    },
    mounted(){
      this.emitter.on('quickDocumentAdd',(document)=>{
        this.grid.api.applyTransaction({add:[document]})
      })
    },
    data(){
        return{
            menu:DocumentMenu(this.createDocument,
                              this.cancelChanges,
                              this.deleteSelected,
                              this.saveChanges,
                              this.loadList,
                              this.quickAdd),
            life:new DocumentLife(),
            grid:new GridClass({
                onCellValueChanged: null,
                onCellClicked: this.onCellClicked,
                onSelectionChanged: this.onSelectionChanged,
              },{
                loadList:this.loadList
              },
              this.services.document
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
          mainOnCellClicked(params, {router:this.$router, life:this.life, service:this.services.document})
          onCellClicked(params, {router:this.$router, service:this.services.document})
        },

        onCellValueChanged(params){
            //
        },
        onSelectionChanged(params){
          //
        },
        createDocument(){
          this.$router.push({name:'DocumentCreate'})
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
          return DocumentColumn()
        },
        setRowCollection(collection, key){
          setRowCollection(collection, key, {grid:this.grid, life:this.life})
        },
        quickAdd(){
          this.$route.name === 'DocumentList'
              ?
              this.$router.push({name: 'DocumentQuickCreate'})
              :
              this.$router.push({name: 'DocumentList'})
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
.boldText{
  font-size:0.9em!important;
}
</style>
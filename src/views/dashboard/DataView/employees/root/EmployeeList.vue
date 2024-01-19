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
          owner-key="employee"
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

import EmployeesColumns from "@/views/dashboard/DataView/employees/EmployeeColumns";
import defaultColDef from "@/utils/grid/defaultColDef";
import {getCurrentInstance, inject, onMounted, ref} from "vue";
import EmployeeMenu from "@/views/dashboard/DataView/employees/EmployeeMenu";
import EmployeeLife from "@/views/dashboard/DataView/employees/EmployeeLife";
import onCellClicked from "@/views/dashboard/DataView/employees/functions/onCellClicked";
import onCellValueChanged from "@/views/dashboard/DataView/employees/functions/onCellValueChanged";
import EmployeeCrudController from "@/views/dashboard/DataView/employees/controllers/EmployeeCrudController";
import {EmployeeService} from "@/services/EmployeeService";
import getAllRows from "@/utils/grid/getAllRows";
import GridClass from "@/utils/grid/GridClass";
import CompaniesColumns from "@/views/dashboard/DataView/companies/CompanyColumns";
import CollectionRenderer from "@/components/CollectionManager/CollectionRenderer.vue";
import Toast from "@/utils/classes/Toast";
import EditOptions from "@/views/dashboard/DataView/employees/options/EditOptions";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";
import setRowCollection from "@/utils/grid/setRowCollection";

const componentRef = ref(null);
function handleClick(params, del) {
    if(componentRef.value)
     componentRef.value.onCellValueChanged(params, del);
}

export default {
    name: "EmployeeList",
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
          emp: new EmployeeService(emitter)
        };
        const crud = new EmployeeCrudController(services.emp)
        return {
          services,
          crud,
          emitter,
          toast
        }
    },
    mounted(){
      this.emitter.on('quickEmployeeAdd',(employee)=>{
        this.grid.api.applyTransaction({add:[employee]})
      })
    },
    data(){
        return{
            menu:EmployeeMenu(this.createEmployee,
                              this.cancelChanges,
                              this.deleteSelected,
                              this.saveChanges,
                              this.loadList,
                              this.quickAdd),
            life:new EmployeeLife(),
            grid:new GridClass({
                onCellValueChanged: null,
                onCellClicked: this.onCellClicked,
                onSelectionChanged: this.onSelectionChanged,
              },{
                loadList:this.loadList
              },
              this.services.emp
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
        createEmployee(){
          this.$router.push({name:'EmployeeCreate'})
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
          return EmployeesColumns()
        },
        setRowCollection(collection, key){
          setRowCollection(collection, key, {grid:this.grid, life:this.life})
        },
        quickAdd(){
          this.$route.name === 'EmployeeList'
              ?
              this.$router.push({name: 'EmployeeQuickCreate'})
              :
              this.$router.push({name: 'EmployeeList'})
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
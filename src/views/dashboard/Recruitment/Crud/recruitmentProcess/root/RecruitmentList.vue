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
        owner-key="recruitment"
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
import {RecruitmentService} from "@/services/RecruitmentService";
import Toast from "@/utils/classes/Toast";

import GridClass from "@/utils/grid/GridClass";
import mainOnCellClicked from "@/utils/grid/mainOnCellClicked";


import setRowCollection from "@/utils/grid/setRowCollection";
import RecruitmentLife from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentLife";
import RecruitmentCrudController
  from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/controllers/RecruitmentCrudController";
import RecruitmentMenu from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentMenu";
import EditOptions from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/options/EditOptions";
import onCellClicked from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/functions/onCellClicked";
import RecruitmentColumns from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentColumns";

const componentRef = ref(null);
function handleClick(params, del) {
    if(componentRef.value)
        componentRef.value.onCellValueChanged(params, del);
}

export default {
    name: "RecruitmentList",
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
          com:new RecruitmentService(emitter)
        };
        const toast = new Toast()
        const crud = new RecruitmentCrudController(services.com,toast)
        return {
          services,
          crud,
          emitter,
          toast
        }
    },

    mounted(){
      this.emitter.on('quickRecruitmentAdd',(recruitment)=>{
        this.grid.api.applyTransaction({add:[recruitment]})
      })
    },
    data(){
        return{
            menu:RecruitmentMenu(this.createRecruitment,
                              this.cancelChanges,
                              this.deleteSelected,
                              this.saveChanges,
                              this.loadList,
                              this.quickAdd),
            life:new RecruitmentLife(),
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
      createRecruitment(){
         this.$router.push({name:'RecruitmentCreate'})
      },
      cancelChanges(){
         console.log('cancel changes')
      },
      async saveChanges(){
          console.log('save changes')
      },
      getColumns(){
        return RecruitmentColumns()
      },
      setRowCollection(collection, key){
        setRowCollection(collection, key, {grid:this.grid, life:this.life})
      },
      quickAdd(){
        this.$route.name === 'RecruitmentList'
            ?
            this.$router.push({name: 'RecruitmentQuickCreate'})
            :
            this.$router.push({name: 'RecruitmentList'})
      }
    }
}
</script>
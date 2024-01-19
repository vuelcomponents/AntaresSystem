<template>
  <div class="morro">
    <span>
      <p class="form-label">Opened recruitment</p>
    </span>
    <InputSwitch v-model="recruitment.open" @change="this.services.com.update(this.recruitment)" />
  </div>
  <div>
  </div>
  <div>
    <ag-grid-vue
        class="ag-theme-alpine _grid grid-black"
        style="width: 100%"
        domLayout="autoHeight"
        :rowData="grid.data"
        suppressSizeToFit=true
        :grid-options="grid.options"
        rowSelection="multiple"
        animateRows="true"
        :columnDefs="columns"
    >
    </ag-grid-vue>
  </div>
  <MatchingStage :recruitment-variants="recruitment.variants" :close="close" :employee="life.matchingEmployee" v-if="life.matchingEmployee"></MatchingStage>
  <DocumentStage :life="life" :recruitment="recruitment" :set-collection="setDocumentCollection" :close="close" :employee="life.documentEmployee" v-if="life.documentEmployee"></DocumentStage>
  <ContactStage  :close="close" :employee="life.contactEmployee" v-if="life.contactEmployee"></ContactStage>
  <DecideStage  :close="close" :employee="life.decideEmployee" v-if="life.decideEmployee"></DecideStage>

</template>

<script>


import { inject } from "vue";
import Toast from "@/utils/classes/Toast";
import {RecruitmentService} from "@/services/RecruitmentService";
import RecruitmentCrudController
  from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/controllers/RecruitmentCrudController";
import RecruitmentLife from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentLife";
import RecruitmentClass from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentClass";
import EmployeeShortColumn from "@/views/dashboard/DataView/employees/EmployeeShortColumn";
import GridClass from "@/utils/grid/GridClass";
import REmployeeColumns from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/REmployeeColumns";
import onCellClicked from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/functions/onCellClicked";
import SharkStepper from "@/components/SharkStepper.vue";
import MatchingStage from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/root/MatchingStage.vue";
import ContactStage from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/root/ContactStage.vue";
import DecideStage from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/root/DecideStage.vue";
import DocumentStage from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/root/DocumentStage.vue";
import {EmployeeService} from "@/services/EmployeeService";
import EmployeeCrudController from "@/views/dashboard/DataView/employees/controllers/EmployeeCrudController";

export default {
  components: {MatchingStage, DecideStage, ContactStage, DocumentStage},
  setup(){
    const emitter = inject('emitter')
    const services = {
      com: new RecruitmentService(emitter),
      emp:new EmployeeService(emitter)
    };
    const toast = new Toast();
    const crud = new RecruitmentCrudController(services.com, {toast})
    const employeeCrud = new EmployeeCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter,
      employeeCrud
    }
  },
  data() {
    return {
      grid:new GridClass({
            onCellValueChanged: ()=>{
              console.log('KURWA')},
            onCellClicked: this.onCellClicked,
            onSelectionChanged: ()=>{},
        //this.onSelectionChanged ??
          },{
            loadList:this.loadList
          },
          this.services.com

      ),
      columns:[],
      life:new RecruitmentLife(),
      stItems:[{
        title:'Recruitment',
        mdi:'mdi-briefcase',
        id:'___begin'
      }],
      recruitment: new RecruitmentClass(),
    }
  },
  async mounted(){
    this.recruitment = await this.crud.getById(this.$route.params.id)
    console.log('rr', this.recruitment)
    this.recruitment.stages?.forEach((stage, index)=>{
      this.stItems.push({
        title:stage?.code,
        headerName:stage.code,
        field:'stage.code',
        id:stage.code,
         mdi:'mdi-foot-print',
        stage
      })
    })
  },
  methods:{
    getColumns(){
      this.columns = REmployeeColumns(this.recruitment)
    },
    setDocumentCollection(collection, key){
      if(!key){
        this.life.documentEmployee = null;
      }
      if(collection) {
        this.employeeCrud.getById(this.life.documentEmployee.id).then(res=>{
          this.life.documentEmployee = res
          this.grid.updateRowById(res.id, res)
        })
      }
    },
    async loadList(){
      this.recruitment = await this.crud.getById(this.$route.params.id)
      const list = this.recruitment.employees;
      this.grid.api.setRowData(list);
      this.getColumns()
    },
    onCellClicked(params){
      onCellClicked(params,
          {
              services:{rec:this.services.com, emp:this.services.emp},
              router:this.$router, toast:this.toast, life:this.life,
              loadList:this.loadList
            }
          )
    },
    close(event){
      switch(event.target.id){
        case "matching-stage-container":
          this.life.matchingEmployee = null;
          break;
        case "document-stage-container":
          this.life.documentEmployee = null;
          break;
        case "decide-stage-container":
          this.life.decideEmployee = null;
          break;
        case "contact-stage-container":
          this.life.contactEmployee = null;
          break;
      }
    }
  }
}
</script>



<style>
.morro{
  color:#fff;
  text-align:left;
  padding:10px;
}
.stage-container{
  width:100vw;
  height:100vh;
  position:fixed;
  top:0;
  left:0;
  z-index:20;
  background:#00000030;
  display:flex;
  justify-content:center;
  align-items:center;
  main{
    margin-top:70px;
    min-width:100px;
    background:#fff;
    min-height:100px;
    border-radius:6px;
    max-width:85vw;
    max-height:85vh;
    overflow-y:auto;
    align-items:flex-start;
    justify-content:flex-start;
    display:flex;
    color:#000!important;
  }
}
</style>
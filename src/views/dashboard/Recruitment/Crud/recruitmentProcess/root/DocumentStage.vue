<script >
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";
import {EmployeeService} from "@/services/EmployeeService";
import {inject} from "vue";

export default{
  name:"DocumentStage",
  computed: {
    EmployeeService() {
      return EmployeeService
    }
  },
  setup(){
    const emitter = inject('emitter')
    return {
      emitter
    }
  },
  data(){
    return{
      requiredDocumentsQty:0,
      stillRequired:[]
    }
  },
  mounted(){
    this.loadValidators()
  },
  props:{
    employee:undefined,
    recruitment:undefined,
    life:undefined,
    close:Function,
    setCollection:Function,
  },
  methods:{
    loadValidators(){
      this.requiredDocumentsQty =
          this.life.documentEmployee.documents.filter(d=>this.recruitment.documentTypes.some(dt=>dt.id === d.documentType?.id)).length;
      this.stillRequired = this.recruitment.documentTypes.filter(dt=> !(this.life.documentEmployee.documents.some( d=>d.documentType?.id === dt.id)))
          .map(r => ({ code: r.code }));
    }
  },
  watch:{
    "life.documentEmployee"(){
      this.loadValidators()
    }
  }
}
</script>

<template>
  <section class="stage-container" id="document-stage-container" @click="close" :key="stillRequired">
    <main style="display:flex;flex-direction:column">
      <div style="background:#062124;  color:#fff; width:100%; padding:15px; display:flex;justify-content:flex-start; ">
        <span style="background:#1f383b;border:solid 3px #fff; border-radius:4px; padding-inline:40px; padding-block:10px;">
           Fulfilled documents: {{    this.requiredDocumentsQty }} / {{recruitment.documentTypes.length}}
        </span>
        <span style=" padding-inline:40px; padding-block:10px;">
          <p style="font-size:0.7em; color:#eaea78; display:inline" v-for="req in stillRequired" :key="req">
            * &nbsp; {{req.code}},
          </p>
          <p v-if="stillRequired.length>0" style="font-size:0.7em; color:#eaea78; display:inline">
            still needs to be uploaded
          </p>
        </span>
      </div>
      <CollectionManager :modules="[
                                   life.documentEmployee.create.includes('documents') ? 'create' : '',
                                   life.documentEmployee.select.includes('documents') ? 'select' : '',
                                   life.documentEmployee.delete.includes('documents') ? 'delete' : '',
                                   life.documentEmployee.update.includes('documents') ? 'update' :'',
                              ]"
                           :no-fixed="true"
                           :service="EmployeeService"
                           :owner="employee"
                           owner-key="employee"
                           :emitter="emitter"
                           :set-collection="setCollection"
                           collection-key="documents"
                           :collection="life.documentEmployee.documents"></CollectionManager>
    </main>
  </section>

</template>

<style >

</style>

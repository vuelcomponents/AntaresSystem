

<template>
<div class="multiple-create">
  <div :hidden="hide(param)" class="box" v-for="param in employee.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{employee.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="employee[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in employee.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{employee.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="employee.getHeaders([param],'first')" class="form-text-input" v-model="employee[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in employee.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{employee.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="employee[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in employee.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{employee.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="employee[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   employee.create.includes(param) ? 'create' : '',
                                   employee.select.includes(param) ? 'select' : '',
                                   employee.delete.includes(param) ? 'delete' : '',
                                   employee.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="employee"
                         owner-key="employee"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="employee[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in employee.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{employee.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="employee" :param="param" :disabled="lock(param)" :model="employee"></SelectionManager>
    </div>
  </div>
  <div v-for="param in employee.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{employee.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="employee.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="employee.setFile($event, param)" />
    </div>
  </div>
  <div  style="display:flex; align-items:flex-end;gap:2px; padding:2px; padding-inline:6px;">
    <button class="button-quick" @click="create" style="margin:0;">
      <i class="mdi mdi-link-plus" />
      Create
    </button>
  </div>
</div>




</template>
<script>
import DataPopup from "@/components/popups/DataPopup.vue";
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";
import { inject } from "vue";
import {EmployeeService} from "@/services/EmployeeService";
import EmployeeCrudController from "@/views/dashboard/DataView/employees/controllers/EmployeeCrudController";
import CreateOptions from "@/views/dashboard/DataView/employees/options/CreateOptions";
import Toast from "@/utils/classes/Toast";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import EmployeeLife from "@/views/dashboard/DataView/employees/EmployeeLife";


export default {
  components: {CollectionManager},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new EmployeeService(emitter)
    };
    const toast = new Toast()
    const crud = new EmployeeCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      employee: new EmployeeClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new EmployeeLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.employee).then(res=>{
        if(res?.invalid){
          console.log('invalid employee create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) this.emitter.emit('quickEmployeeAdd', res.data)
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.employee[key] = collection
      }
    },
    lock(param){
      if(!this.employee.$lock[param]){
        return false;
      }
      return this.employee.$lock[param]()
    },
    hide(param){
      if(!this.employee.$hide[param]){
        return false;
      }
      return this.employee.$hide[param]()
    }
  }
}
</script>
<style >

</style>
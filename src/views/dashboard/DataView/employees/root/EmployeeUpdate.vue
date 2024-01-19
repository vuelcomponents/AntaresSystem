<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in employee.getInputs('text')" :key="param">
          <p class="form-label">
            {{employee.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="employee[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in employee.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{employee.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="employee.getHeaders([param],'first')" class="form-text-input" v-model="employee[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in employee.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{employee.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <Calendar :showIcon="true" :disabled="lock(param)" :key="employee[param]" date-format="yy-mm-dd" v-model="employee[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in employee.getInputs('collections')" :hidden="hide(param)" :key="param">
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
        <div class="box" v-for="param in employee.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{employee.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="employee" :param="param" :disabled="lock(param)" :model="employee"></SelectionManager>
          </div>
        </div>
        <div v-for="param in employee.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
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
      </template>
      <template v-slot:item2contentLeft>
        <div class="box">
         <CompareCompany :employee="this.employee"/>
        </div>
      </template>

      <template v-slot:item2contentRight>
        <div class="box">
        </div>
      </template>

      <template v-slot:item3contentLeft>
        <div class="box">
          left
        </div>
      </template>

      <template v-slot:item3contentRight>
        <div class="box">
          right
        </div>
      </template>
    </DataPopup>
  </div>
</template>

<script>

import DataPopup from "@/components/popups/DataPopup.vue";
import EmployeeClass from "@/views/dashboard/DataView/employees/EmployeeClass";
import EditOptions from "@/views/dashboard/DataView/employees/options/EditOptions";
import { inject } from "vue";
import {EmployeeService} from "@/services/EmployeeService";
import EmployeeCrudController from "@/views/dashboard/DataView/employees/controllers/EmployeeCrudController";
import Toast from "@/utils/classes/Toast";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import RealisationService from "@/services/RealisationService";
import EmployeeLife from "@/views/dashboard/DataView/employees/EmployeeLife";
import CompareCompany from "@/views/dashboard/DataView/employees/root/CompareCompany.vue";


export default {
  components: {CompareCompany, CollectionManager, DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new EmployeeService(emitter),
      realisations: new RealisationService(emitter)
    };
    const toast =new Toast();
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
      life: new EmployeeLife(),
      popupOptions: new EditOptions(this.$router, this.update),
    }
  },
  async mounted(){
    this.employee = await this.crud.getById(this.$route.params.id)
    console.log('read employee', this.employee)
  },
  methods:{
    update(){
      console.log('up', this.employee)
      this.crud.update(this.employee).then(res=>{
        if(res?.invalid){
          console.log('invalid employee create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        // this.employee[key] = collection
        this.crud.getById(this.employee.id).then(res=>{
          this.employee = res
        })
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



<style>

</style>
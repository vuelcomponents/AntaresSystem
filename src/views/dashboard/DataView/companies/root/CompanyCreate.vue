<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in company.getInputs('text')" :key="param">
          <p class="form-label">
            {{company.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="company[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in company.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{company.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="company.getHeaders([param],'first')" class="form-text-input" v-model="company[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in company.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{company.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <ParsingCalendar :disabled="lock(param)" :model="company[param]" :key="company[param]" :setter="(value)=>company.setParam(param, value)" />
        </div>
        <div class="box" v-for="param in company.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{company.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="company[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   company.create.includes(param) ? 'create' : '',
                                   company.select.includes(param) ? 'select' : '',
                                   company.delete.includes(param) ? 'delete' : '',
                                   company.update.includes(param) ? 'update' :'',
                              ]"
                               :service="services[param]"
                               :owner="company"
                               owner-key="company"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="company[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in company.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{company.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="company" :param="param" :disabled="lock(param)" :model="company"></SelectionManager>
          </div>
        </div>
        <div v-for="param in company.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{company.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="company.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="company.setFile($event, param)" />
          </div>
        </div>
      </template>
      <template v-slot:item2contentLeft>
        <div class="box">
          left
        </div>
      </template>

      <template v-slot:item2contentRight>
        <div class="box">
          right
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
import { inject } from "vue";


import CompanyClass from "@/views/dashboard/DataView/companies/CompanyClass";
import {CompanyService} from "@/services/CompanyService";
import CompanyCrudController from "@/views/dashboard/DataView/companies/controllers/CompanyCrudController";
import CreateOptions from "@/views/dashboard/DataView/companies/options/CreateOptions";
import Toast from "@/utils/classes/Toast";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import CompanyLife from "@/views/dashboard/DataView/companies/CompanyLife";


export default {
  components: {CollectionManager, DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      cmp: new CompanyService(emitter)
    };
    const toast = new Toast();
    const crud = new CompanyCrudController(services.cmp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      company: new CompanyClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new CompanyLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.company).then(res=>{
        if(res?.invalid){
          console.log('invalid company create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.company[key] = collection
      }
    },
    lock(param){
      if(!this.company.$lock[param]){
        return false;
      }
      return this.company.$lock[param]()
    },
    hide(param){
      if(!this.company.$hide[param]){
        return false;
      }
      return this.company.$hide[param]()
    }
  }
}
</script>



<style>

</style>
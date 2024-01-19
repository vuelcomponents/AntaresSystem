

<template>
<div class="multiple-create">
  <div :hidden="hide(param)" class="box" v-for="param in recruitment.getInputs('text',null,true)" :key="param">
    <p class="form-label">
      {{recruitment.getHeaders([param],"first")}}
    </p>
    <InputText  :disabled="lock(param)" v-model="recruitment[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in recruitment.getInputs('numeric',null,true)" :key="param" :hidden="hide(param)">
    <p class="form-label">
      {{recruitment.getHeaders([param],"first")}} <span class="mini"> | Number </span>
    </p>
    <InputNumber  :disabled="lock(param)"  :placeholder="recruitment.getHeaders([param],'first')" class="form-text-input" v-model="recruitment[param]"></InputNumber>
  </div>
  <div class="box" v-for="param in recruitment.getInputs('dates',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{recruitment.getHeaders([param],"first")}} <span class="mini"> | Date </span>
    </p>
    <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="recruitment[param]" class="form-text-input"/>
  </div>
  <div class="box" v-for="param in recruitment.getInputs('collections',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{recruitment.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
    </p>
    <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="recruitment[param]"></InputClick>
    <div v-if="life.collectionKey === param">
      <CollectionManager :modules="[
                                   recruitment.create.includes(param) ? 'create' : '',
                                   recruitment.select.includes(param) ? 'select' : '',
                                   recruitment.delete.includes(param) ? 'delete' : '',
                                   recruitment.update.includes(param) ? 'update' :'',
                              ]"
                         :service="services[param]"
                         :owner="recruitment"
                         owner-key="recruitment"
                         :emitter="emitter"
                         :set-collection="setCollection"
                         :collection-key="param"
                         :collection="recruitment[param]"></CollectionManager>
    </div>
  </div>
  <div class="box" v-for="param in recruitment.getInputs('objects',null,true)" :hidden="hide(param)" :key="param">
    <p class="form-label">
      {{recruitment.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <SelectionManager :key="recruitment" :param="param" :disabled="lock(param)" :model="recruitment"></SelectionManager>
    </div>
  </div>
  <div v-for="param in recruitment.getInputs('files',null,true)" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
    <p class="form-label">
      {{recruitment.getHeaders([param],"first")}} <span class="mini"> | Select </span>
    </p>
    <div>
      <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                  :disabled="lock(param)"
                  :chooseLabel="recruitment.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                  :maxFileSize="50000000"  @input="recruitment.setFile($event, param)" />
    </div>
  </div>
  <div  style="display:flex; align-items:flex-end;gap:2px; padding:2px; padding-inline:6px;">
    <button class="button-quick" @click="create" style="margin:0;">
      <i class="mdi mdi-link-plus" />
      Create
    </button>
  </div>
</div>
  <div class="multiple-create" style="display:block" :key="life.counterKey">
    <div  :hidden="hide(param)" class="box" v-for="param in recruitment.getInputs('textArea',null,true)" :key="param">
      <p class="form-label" >
        {{recruitment.getHeaders([param],"first")}}
      </p>
      <Editor  :disabled="lock(param)" v-model="recruitment[param]" class="form-text-input"/>
    </div>
  </div>
</template>
<script>

import { inject } from "vue";

import Toast from "@/utils/classes/Toast";
import RecruitmentCrudController
  from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/controllers/RecruitmentCrudController";
import RecruitmentClass from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentClass";
import CreateOptions from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/options/CreateOptions";
import RecruitmentLife from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentLife";
import {RecruitmentService} from "@/services/RecruitmentService";



export default {
  components: {},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new RecruitmentService(emitter)
    };
    const toast = new Toast()
    const crud = new RecruitmentCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      recruitment: new RecruitmentClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new RecruitmentLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.recruitment).then(res=>{
        if(res?.invalid){
          console.log('invalid recruitment create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          if(res.data) {
            this.emitter.emit('quickRecruitmentAdd', res.data)
          }
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.recruitment[key] = collection
      }
    },
    lock(param){
      if(!this.recruitment.$lock[param]){
        return false;
      }
      return this.recruitment.$lock[param]()
    },
    hide(param){
      if(!this.recruitment.$hide[param]){
        return false;
      }
      return this.recruitment.$hide[param]()
    }
  }
}
</script>
<style >

</style>
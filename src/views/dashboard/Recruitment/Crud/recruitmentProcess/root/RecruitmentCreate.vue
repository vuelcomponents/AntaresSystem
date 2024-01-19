<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in recruitment.getInputs('text')" :key="param">
          <p class="form-label">
            {{recruitment.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="recruitment[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in recruitment.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{recruitment.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="recruitment.getHeaders([param],'first')" class="form-text-input" v-model="recruitment[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in recruitment.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{recruitment.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <ParsingCalendar :disabled="lock(param)" :model="recruitment[param]" :key="recruitment[param]" :setter="(value)=>recruitment.setParam(param, value)" />
        </div>
        <div class="box" v-for="param in recruitment.getInputs('collections')" :hidden="hide(param)" :key="param">
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
        <div class="box" v-for="param in recruitment.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{recruitment.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="recruitment" :param="param" :disabled="lock(param)" :model="recruitment"></SelectionManager>
          </div>
        </div>
        <div v-for="param in recruitment.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
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
import {RecruitmentService} from "@/services/RecruitmentService";
import Toast from "@/utils/classes/Toast";
import RecruitmentCrudController
  from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/controllers/RecruitmentCrudController";
import RecruitmentClass from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentClass";
import CreateOptions from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/options/CreateOptions";
import RecruitmentLife from "@/views/dashboard/Recruitment/Crud/recruitmentProcess/RecruitmentLife";



export default {
  components: { DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      recruitment: new RecruitmentService(emitter)
    };
    const toast = new Toast();
    const crud = new RecruitmentCrudController(services.recruitment, {toast})
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
          history.back()
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



<style>

</style>
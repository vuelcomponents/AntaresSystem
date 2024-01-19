<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in mail.getInputs('text')" :key="param">
          <p class="form-label">
            {{mail.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="mail[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in mail.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{mail.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="mail.getHeaders([param],'first')" class="form-text-input" v-model="mail[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in mail.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{mail.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <ParsingCalendar :disabled="lock(param)" :model="mail[param]" :key="mail[param]" :setter="(value)=>mail.setParam(param, value)" />
        </div>
        <div class="box" v-for="param in mail.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{mail.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="mail[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   mail.create.includes(param) ? 'create' : '',
                                   mail.select.includes(param) ? 'select' : '',
                                   mail.delete.includes(param) ? 'delete' : '',
                                   mail.update.includes(param) ? 'update' :'',
                              ]"
                               :service="services[param]"
                               :owner="mail"
                               owner-key="mail"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="mail[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in mail.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{mail.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="mail" :param="param" :disabled="lock(param)" :model="mail"></SelectionManager>
          </div>
        </div>
        <div v-for="param in mail.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{mail.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="mail.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="mail.setFile($event, param)" />
          </div>
        </div>
        <div  style="display:block">
          <div  :hidden="hide(param)" class="box" v-for="param in mail.getInputs('textAreaHeaders',null,true)" :key="param">
            <p class="form-label" >
              {{mail.getHeaders([param],"first")}}
            </p>
            <InputText  :disabled="lock(param)" v-model="mail[param]" class="form-text-input"/>
          </div>
          <div  :hidden="hide(param)" class="box" v-for="param in mail.getInputs('textArea',null,true)" :key="param">
            <p class="form-label" >
              {{mail.getHeaders([param],"first")}}
            </p>
            <Editor  :disabled="lock(param)" v-model="mail[param]" class="form-text-input"/>
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
import Toast from "@/utils/classes/Toast";
import MailCrudController from "@/views/dashboard/DataView/mails/controllers/MailCrudController";
import MailLife from "@/views/dashboard/DataView/mails/MailLife";
import MailClass from "@/views/dashboard/DataView/mails/MailClass";
import EditOptions from "@/views/dashboard/DataView/mails/options/EditOptions";
import {MailService} from "@/services/MailService";
export default {
  components: {DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      com: new MailService(emitter)
    };
    const toast = new Toast();
    const crud = new MailCrudController(services.com, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      life:new MailLife(),
      mail: new MailClass(),
      popupOptions: new EditOptions(this.$router, this.update),
    }
  },
  async mounted(){
    this.mail = await this.crud.getById(this.$route.params.id)
  },
  methods:{
    update(){
      this.crud.update(this.mail).then(res=>{
        if(res?.invalid){
          console.log('invalid mail create, empty fields', res.invalid)
        }
        if(res.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.mail[key] = collection
      }
    },
    lock(param){
      if(!this.mail.$lock[param]){
        return false;
      }
      return this.mail.$lock[param]()
    },
    hide(param){
      if(!this.mail.$hide[param]){
        return false;
      }
      return this.mail.$hide[param]()
    }
  }
}
</script>



<style>

</style>
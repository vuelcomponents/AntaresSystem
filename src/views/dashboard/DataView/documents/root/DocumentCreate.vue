<template>
  <div>
    <DataPopup :menuItems="popupOptions.menuItems" :stepperSettings="popupOptions.stepperSettings">
      <template v-slot:item1contentLeft>
        <div :hidden="hide(param)" class="box" v-for="param in document.getInputs('text')" :key="param">
          <p class="form-label">
            {{document.getHeaders([param],"first")}}
          </p>
          <InputText  :disabled="lock(param)" v-model="document[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in document.getInputs('numeric')" :key="param" :hidden="hide(param)">
          <p class="form-label">
            {{document.getHeaders([param],"first")}} <span class="mini"> | Number </span>
          </p>
          <InputNumber  :disabled="lock(param)"  :placeholder="document.getHeaders([param],'first')" class="form-text-input" v-model="document[param]"></InputNumber>
        </div>
      </template>
      <template v-slot:item1contentRight>
        <div class="box" v-for="param in document.getInputs('dates')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{document.getHeaders([param],"first")}} <span class="mini"> | Date </span>
          </p>
          <Calendar :disabled="lock(param)" date-format="yy-mm-dd" v-model="document[param]" class="form-text-input"/>
        </div>
        <div class="box" v-for="param in document.getInputs('collections')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{document.getHeaders([param],"first")}} <span class="mini"> | Collection </span>
          </p>
          <InputClick format="collection" :disabled="lock(param)" @click="setCollection(null, param)" :value="document[param]"></InputClick>
          <div v-if="life.collectionKey === param">
            <CollectionManager :modules="[
                                   document.create.includes(param) ? 'create' : '',
                                   document.select.includes(param) ? 'select' : '',
                                   document.delete.includes(param) ? 'delete' : '',
                                   document.update.includes(param) ? 'update' :'',
                              ]"
                               :service="services[param]"
                               :owner="document"
                               owner-key="document"
                               :emitter="emitter"
                               :set-collection="setCollection"
                               :collection-key="param"
                               :collection="document[param]"></CollectionManager>
          </div>
        </div>
        <div class="box" v-for="param in document.getInputs('objects')" :hidden="hide(param)" :key="param">
          <p class="form-label">
            {{document.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <SelectionManager :key="document" :param="param" :disabled="lock(param)" :model="document"></SelectionManager>
          </div>
        </div>
        <div v-for="param in document.getInputs('files')" :key="param" :hidden="hide(param)" style="display:flex;justify-content:center;flex-direction:column">
          <p class="form-label">
            {{document.getHeaders([param],"first")}} <span class="mini"> | Select </span>
          </p>
          <div>
            <FileUpload style="padding-inline:5px;display:flex;gap:8px;height:35px;"
                        :disabled="lock(param)"
                        :chooseLabel="document.getFileName(param) ?? 'Choose file'" mode="basic" name="demo[]"
                        :maxFileSize="50000000"  @input="document.setFile($event, param)" />
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
import {DocumentService} from "@/services/DocumentService";
import DocumentCrudController from "@/views/dashboard/DataView/documents/controllers/DocumentCrudController";
import CreateOptions from "@/views/dashboard/DataView/documents/options/CreateOptions";
import Toast from "@/utils/classes/Toast";
import CollectionManager from "@/components/CollectionManager/CollectionManager.vue";
import DocumentLife from "@/views/dashboard/DataView/documents/DocumentLife";
import DocumentClass from "@/views/dashboard/DataView/DocumentClass";


export default {
  components: {CollectionManager, DataPopup},
  setup(){
    const emitter = inject('emitter')
    const services = {
      emp: new DocumentService(emitter)
    };
    const toast = new Toast()
    const crud = new DocumentCrudController(services.emp, {toast})
    return {
      services,
      crud,
      toast,
      emitter
    }
  },
  data() {
    return {
      document: new DocumentClass(),
      popupOptions: new CreateOptions(this.$router, this.create),
      life: new DocumentLife()
    }
  },
  methods:{
    create(){
      this.crud.create(this.document).then(res=>{
        if(res?.invalid){
          console.log('invalid document create, empty fields', res.invalid)
        }
        if(res?.status === 200) {
          history.back()
        }
      })
    },
    setCollection(collection, key){
      this.life.collectionKey = key
      if(collection) {
        this.document[key] = collection
      }
    },
    lock(param){
      if(!this.document.$lock[param]){
        return false;
      }
      return this.document.$lock[param]()
    },
    hide(param){
      if(!this.document.$hide[param]){
        return false;
      }
      return this.document.$hide[param]()
    }
  }
}
</script>



<style>

</style>
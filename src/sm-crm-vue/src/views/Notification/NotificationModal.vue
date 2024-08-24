<script setup>
    import { ref, computed, onMounted, watch } from 'vue'
    import '@/plugins/axios'
    import '@/plugins/notification'
    import { Clone } from '@/plugins/helper'

    const props = defineProps(['item'])
    const emit = defineEmits(['onSaved'])

    const downloadProfileImageUrl = ref()
    const allTitles = ref()
    const allStatusTypes = ref()
    const allCustomerTypes = ref([{ id: 1, name: 'Corporate' }, { id: 2, name: 'Personel' }])

    onMounted(() => {
        axios.get('/Titles').then(result => allTitles.value = result.data)
        //axios.get('/StatusTypes').then(result => allStatusTypes.value = result.data)
    })

    const isValid = computed(() => {
        return props.item.companyName != null &&
            props.item.companyName != '' &&
            props.item.titleId > 0
    })

    watch(() => props.item && props.item.profileImageUrl, (fileName) => {
        if (fileName) downloadProfileImageUrl.value = getUploadFilePath(fileName);
        else downloadProfileImageUrl.value = getUploadFilePath("default.jpg");

    })

    function saveItem() {
        if (!isValid.value) {
            toastr.warning('Form data is not valid!')
            return;
        }

        const item = Clone(props.item);
        if (item.id > 0) {
            axios.put("/Notification/" + item.id, item).then(res => {
                toastr.success("Notification Updated!", "Updated")
                emit("onSaved", item)

                qw
                easdasd
            })
        } else {
            axios.post("/Notification", item).then(res => {
                toastr.success("Notification created!", "Created")
                emit("onSaved", item)
            })
        }
    }

    function handleFileUpload(event) {
        let selectedFile = event.target.files[0]

        const formData = new FormData()
        formData.append("CustomerId", props.item.id)
        formData.append("ProfileImage", selectedFile)

        axios.post("/Customers/uploadprofileimage", formData).then(res => {
            toastr.success("Uploaded!", "Created")
            downloadProfileImageUrl.value = getUploadFilePath(res.data);
        })
    }

    function getUploadFilePath(fileName) {
        return axios.defaults.baseURL + '/Customers/downloadprofileImage?fileName=' + fileName
    }
</script>
<template>
    <div class="modal modal-blur fade" tabindex="-1" role="dialog" id="appModal">
        <div class="modal-dialog">
            <div class="modal-content" v-if="item">
                <form @submit.prevent="true">
                    <div class="modal-header">
                        <h5 class="modal-title" v-text="item.id > 0 ? 'Edit' : 'New'"></h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <input type="hidden" v-model="item.id" />
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Title</label>
                            <div class="col-lg-9">
                                <input v-model="item.title" class="form-control" />
                            </div>
                        </div>
                        <div class="row mb-3">
                            <label class="col-lg-3 col-form-label">Description</label>
                            <div class="col-lg-9">
                                <input v-model="item.description" class="form-control" />
                            </div>
                        </div>



                    </div>
                    <div class="modal-footer">
                        <button class="btn me-auto" data-bs-dismiss="modal">Close</button>
                        <button class="btn btn-primary"
                                v-text="item.id > 0 ? 'Update' : 'Save'"
                                @click="saveItem"
                                :disabled="!isValid"></button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>
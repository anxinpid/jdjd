import { Form, Input, Radio, DatePicker, TreeSelect, Modal } from 'antd';
import React, { useEffect, useState } from 'react';
import { BaojiaPutAPI, BaojiaPostAPI } from "../../http/api"

const View = (props) => {
    const [form] = Form.useForm()

    useEffect(
        () => {
            if (props.open) {
                form.setFieldsValue(props.selectedRecord);
            } else {
                if (!props.open) form.resetFields();
            }
        },
        [props.open]
    )

    const onCreate = (values) => {
        // console.log('Received values of form: ', values)
        // console.log({ ...values, Id: props.selectedRecord.Id });
      
        BaojiaPostAPI({ ...values, Id: props.selectedRecord.Id }).then(resJson => {
            props.onCancel();
        });
    };

    return (
        <Modal
            forceRender
            open={props.open}
            title="编辑表单"

            onCancel={props.onCancel}
            onOk={() => {
                form
                    .validateFields()
                    .then((values) => {
                        onCreate(values);
                    })
                    .catch((info) => {
                        console.log('Validate Failed:', info);
                    });
            }}
        >
            <Form
                form={form}
                layout="vertical"
                name="form_in_modal"
                initialValues={{
                    modifier: 'public',
                }}
            >
                <Form.Item label="品牌" name="BrandName" >
                    <Input />
                </Form.Item>
                <Form.Item label="型号" name="BrandFullName" >
                    <Input />
                </Form.Item>
                <Form.Item label="jd地址" name="JdUrl" >
                    <Input />
                </Form.Item>
                <Form.Item label="jd价格" name="JdPrice" >
                    <Input />
                </Form.Item>
                <Form.Item label="渠道底价" name="QdPrice" >
                    <Input />
                </Form.Item>
                <Form.Item label="参考价格" name="ReferPrice" >
                    <Input />
                </Form.Item>
                <Form.Item label="询价日期" name="Riqi" >
                    <Input />
                </Form.Item>
                <Form.Item label="询价人" name="weName" >
                    <Input />
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default View

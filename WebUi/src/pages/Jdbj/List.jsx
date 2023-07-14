import { Space, Table, Button } from 'antd';
import { PlusOutlined } from '@ant-design/icons'
import { useEffect, useState } from 'react';
import View from './EditForm.jsx'


import { BaojiaGetAPI } from "../../http/api"


function List() {
  const [data, setData] = useState([]);
  const [open, setOpen] = useState(false)
  const [selectedRecord, setSelectedRecord] = useState()

  const openInNewTab = url => {
    window.open(url, '_blank', 'noopener,noreferrer');
  };

  const columns = [
    {
      title: 'Id',
      dataIndex: 'Id',
      key: 'Id',
    },
    {
      title: '品牌',
      dataIndex: 'BrandName',
      key: 'BrandName',
      render: (text, record) => <a onClick={e => handleClick(record, e)}>{text}</a>,
    },
    {
      title: '型号',
      dataIndex: 'BrandFullName',
      key: 'BrandFullName',
    },
    {
      title: 'jd地址',
      key: 'JdUrl',
      dataIndex: 'JdUrl',
      render: (text, record) => <a onClick={e => openInNewTab(text)}>{text}</a>,

    },
    {
      title: 'jd价格',
      key: 'JdPrice',
      dataIndex: 'JdPrice',
    },
    {
      title: '渠道底价',
      key: 'QdPrice',
      dataIndex: 'QdPrice',
    },
    {
      title: '参考价格',
      key: 'ReferPrice',
      dataIndex: 'ReferPrice',
    },
    {
      title: '询价日期',
      key: 'Riqi',
      dataIndex: 'Riqi',
    },
    {
      title: '询价微信',
      key: 'WeName',
      dataIndex: 'WeName',
    },
  ];


  const getDataList = () => {
    BaojiaGetAPI().then((res) => {
      console.log(res.Data)
      setData(res.Data);
    });
  }
  useEffect(() => {
    getDataList()
  }, [])


  //点击
  const handleClick = (record, e) => {
    setSelectedRecord(record)
    setOpen(true)
  }


  return <>
    <View
      open={open}
      onCancel={() => {
        setOpen(false)
        getDataList()
      }}
      selectedRecord={selectedRecord}
    />


    <Button onClick={() => {
      setSelectedRecord()
      setOpen(true)
    }}>
      <PlusOutlined />新建</Button>

    <Table columns={columns} dataSource={data} rowKey={record => record.Id} />
  </>

}
export default List
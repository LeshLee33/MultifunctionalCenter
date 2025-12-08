from src.server.database import create, read, update, delete, service


class TestClassDBFunctions:
    def test_data_creation(self):
        data = dict(service_type="test_service_type",
                    service_title="test_service_title",
                    service_required_documents=["test_document_1", "test_document_2"],
                    service_government_structure="test_government_structure",
                    service_duration_days=0)
        result = create(collection=service, data=data)
        print(result)
        assert result['code'] == 0

    def test_one_data_reading(self):
        query = dict(service_title="test_service_title")
        result = read(collection=service, query=query)
        assert len(result) == 1


    def test_many_data_reading(self):
        query = dict(service_type="Паспортные услуги")
        result = read(collection=service, query=query)
        assert len(result) > 1


    def test_none_data_reading(self):
        query = dict(service_type="Паспортны услуг")
        result = read(collection=service, query=query)
        assert result == []


    def test_partially_matching_data_reading(self):
        query = dict(service_title={"$regex": "Регистрация"})
        result = read(collection=service, query=query)
        assert len(result) >= 1


    def test_data_updating(self):
        query = dict(service_title="test_service_title")
        new_data = {"$set": dict(service_type="test_type")}
        result = update(collection=service, query=query, new_data=new_data)
        assert result['code'] == 0


    def test_data_deletion(self):
        query = dict(service_title="test_service_title")
        result = delete(collection=service, query=query)
        assert result['code'] == 0

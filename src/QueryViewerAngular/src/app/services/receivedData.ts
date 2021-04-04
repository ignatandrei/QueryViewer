export interface receivedData {
    queryName:  string;
    itemName:   string;
    fieldNames: FieldName[];
    values:     any[];
}

export interface FieldName {
    itemName:     string;
    queryName:    string;
    fieldName:    string;
    fieldType:    string;
    defaultValue: DefaultValue | null;
}

export interface DefaultValue {
    fieldName:      string;
    value:          string;
    criteria:       number;
    criteriaString: string;
}

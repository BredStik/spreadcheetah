﻿//HintName: MyNamespace.MyGenRowContext.g.cs
// <auto-generated />
#nullable enable
using SpreadCheetah;
using SpreadCheetah.SourceGeneration;
using SpreadCheetah.Styling;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MyNamespace
{
    public partial class MyGenRowContext
    {
        private static MyGenRowContext? _default;
        public static MyGenRowContext Default => _default ??= new MyGenRowContext();

        public MyGenRowContext()
        {
        }
        private static readonly MyNamespace.StringValueConverter _valueConverter0 = new MyNamespace.StringValueConverter(); 
        private static readonly MyNamespace.DecimalValueConverter _valueConverter1 = new MyNamespace.DecimalValueConverter(); 

        private WorksheetRowTypeInfo<MyNamespace.ClassWithReusedConverter>? _ClassWithReusedConverter;
        public WorksheetRowTypeInfo<MyNamespace.ClassWithReusedConverter> ClassWithReusedConverter => _ClassWithReusedConverter
            ??= WorksheetRowMetadataServices.CreateObjectInfo<MyNamespace.ClassWithReusedConverter>(
                AddHeaderRow0Async, AddAsRowAsync, AddRangeAsRowsAsync, null);

        private static async ValueTask AddHeaderRow0Async(SpreadCheetah.Spreadsheet spreadsheet, SpreadCheetah.Styling.StyleId? styleId, CancellationToken token)
        {
            var headerNames = ArrayPool<string?>.Shared.Rent(3);
            try
            {
                headerNames[0] = "Property1";
                headerNames[1] = "Property2";
                headerNames[2] = "Property3";
                await spreadsheet.AddHeaderRowAsync(headerNames.AsMemory(0, 3), styleId, token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<string?>.Shared.Return(headerNames, true);
            }
        }

        private static ValueTask AddAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet, MyNamespace.ClassWithReusedConverter? obj, CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (obj is null)
                return spreadsheet.AddRowAsync(ReadOnlyMemory<DataCell>.Empty, token);
            return AddAsRowInternalAsync(spreadsheet, obj, token);
        }

        private static ValueTask AddRangeAsRowsAsync(SpreadCheetah.Spreadsheet spreadsheet,
            IEnumerable<MyNamespace.ClassWithReusedConverter?> objs,
            CancellationToken token)
        {
            if (spreadsheet is null)
                throw new ArgumentNullException(nameof(spreadsheet));
            if (objs is null)
                throw new ArgumentNullException(nameof(objs));
            return AddRangeAsRowsInternalAsync(spreadsheet, objs, token);
        }

        private static async ValueTask AddAsRowInternalAsync(SpreadCheetah.Spreadsheet spreadsheet,
            MyNamespace.ClassWithReusedConverter obj,
            CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(3);
            try
            {
                var styleIds = Array.Empty<StyleId>();
                await AddCellsAsRowAsync(spreadsheet, obj, cells, styleIds, token).ConfigureAwait(false);
            }
            finally
            {
                ArrayPool<DataCell>.Shared.Return(cells, true);
            }
        }

        private static async ValueTask AddRangeAsRowsInternalAsync(SpreadCheetah.Spreadsheet spreadsheet,
            IEnumerable<MyNamespace.ClassWithReusedConverter?> objs,
            CancellationToken token)
        {
            var cells = ArrayPool<DataCell>.Shared.Rent(3);
            try
            {
                var styleIds = Array.Empty<StyleId>();
                foreach (var obj in objs)
                {
                    await AddCellsAsRowAsync(spreadsheet, obj, cells, styleIds, token).ConfigureAwait(false);
                }
            }
            finally
            {
                ArrayPool<DataCell>.Shared.Return(cells, true);
            }
        }

        private static ValueTask AddCellsAsRowAsync(SpreadCheetah.Spreadsheet spreadsheet,
            MyNamespace.ClassWithReusedConverter? obj,
            DataCell[] cells, IReadOnlyList<StyleId> styleIds, CancellationToken token)
        {
            if (obj is null)
                return spreadsheet.AddRowAsync(ReadOnlyMemory<DataCell>.Empty, token);

            cells[0] = _valueConverter0.ConvertToDataCell(obj.Property1);
            cells[1] = _valueConverter1.ConvertToDataCell(obj.Property2);
            cells[2] = _valueConverter0.ConvertToDataCell(obj.Property3);
            return spreadsheet.AddRowAsync(cells.AsMemory(0, 3), token);
        }

        private static DataCell ConstructTruncatedDataCell(string? value, int truncateLength)
        {
            return value is null || value.Length <= truncateLength
                ? new DataCell(value)
                : new DataCell(value.AsMemory(0, truncateLength));
        }
    }
}

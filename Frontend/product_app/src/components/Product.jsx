const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || "https://localhost:7212/api";
import React, { useState, useEffect } from 'react'
import axios from 'axios'

function Product() {
  const [loading, setLoading] = useState(false)
  const [error, setError] = useState(null)
  const [success, setSuccess] = useState(null)
  const [form, setForm] = useState({ name: '', price: '' })
  const [editingId, setEditingId] = useState(null)
  const [products, setProducts] = useState([])
  const [selectedProductIndex, setSelectedProductIndex] = useState(null) 

  const fetchProducts = async () => {
    setLoading(true)
    setError(null)
    try {
      const response = await axios.get(`${API_BASE_URL}/ProductAPI`, {
        headers: {
          'Content-Type': 'application/json',
          'Accept': 'application/json'
        }
      })
      setProducts(response.data)
    } catch (error) {
      setError('Error fetching products')
      console.error('Error fetching products:', error)
    } finally {
      setLoading(false)
    }
  }

  const handleDropdownChange = (e) => {
    const selectedId = e.target.value
    if (selectedId === '') {
      setEditingId(null)
      setForm({ name: '', price: '' })
      setError(null)
    } else {
      const selectedProduct = products.find(p => p.id.toString() === selectedId)
      if (selectedProduct) {
        setForm({ name: selectedProduct.name, price: selectedProduct.price })
        setEditingId(selectedProduct.id)
        setError(null)
      }
    }
  }

  

  useEffect(() => {
    fetchProducts()
  }, [])

  const handleInputChange = (e) => {
    const { name, value } = e.target
    setForm(prev => ({ ...prev, [name]: value }))
  }

  const handleSubmit = async (e) => {
    e.preventDefault()
    if (!form.name || !form.price) {
      setError('fill first')
      return
    }
    setLoading(true)
    setError(null)
    setSuccess(null)
    try {
      if (editingId) {
       
        await axios.put(`${API_BASE_URL}/ProductAPI`, {
          pId: editingId,
          PName: form.name,
          Price: Number(form.price)
        }, {
          headers: { 'Content-Type': 'application/json' }
        })
        setSuccess('Product updated successfully')
      } else {
       
        await axios.post(`${API_BASE_URL}/ProductAPI`, {
          pName: form.name,
          price: Number(form.price)
        }, {
          headers: { 'Content-Type': 'application/json' }
        })
        setSuccess('Product added successfully')
      }
      setForm({ name: '', price: '' })
      setEditingId(null)
      fetchProducts()
      setTimeout(() => setSuccess(null), 3000)
    } catch (error) {
      setError('Error saving product')
      if (error.response) {
        console.error('API error response:', error.response.data)
      } else {
        console.error('Error saving product:', error.message)
      }
    } finally {
      setLoading(false)
    }
  } //--can also be hadled by ternary operator.

  const handleEdit = (product, index) => {
    setForm({ name: product.name, price: product.price })
    setEditingId(product.id)
    setSelectedProductIndex(index) 
    setError(null)
  }

  const handleDelete = async (id) => {
    setLoading(true)
    setError(null)
    setSuccess(null)
    try {
      await axios.delete(`${API_BASE_URL}/ProductAPI/${id}`)
      setSuccess('deleted successfully')
      fetchProducts()
      setTimeout(() => setSuccess(null), 3000)
    } catch (error) {
      setError('Error deleting product')
      console.error('Error deleting product:', error)
    } finally {
      setLoading(false)
    }
  }

  return (
    <>
      <h2>All products</h2>
      {loading && <p className="loading">Loading...</p>}
      {error && <p className="error">{error}</p>}
      {success && <p className="success">{success}</p>}

      <select onChange={handleDropdownChange} value={editingId || ''} style={{ marginBottom: '1rem', padding: '0.5rem' }}>
        <option value="">Add new product</option>
        {products.map(product => (
          <option key={product.id} value={product.id}>
            {product.name}
          </option>
        ))}
      </select>

      <form className="product-form" onSubmit={handleSubmit}>
        <input
          type="text"
          name="name"
          placeholder="Product name"
          value={form.name}
          onChange={handleInputChange}
        />
        <input
          type="number"
          name="price"
          placeholder="Product price"
          value={form.price}
          onChange={handleInputChange}
        />
        <button type="submit">{editingId ? 'Update' : 'Add'} Product</button>
        {editingId && (
          <button
            type="button"
            onClick={() => {
              setEditingId(null)
              setForm({ name: '', price: '' })
              setError(null)
            }}
          >    //-- can also be handle by ternary operator.
            Cancel
          </button>
        )}
      </form>

      <ul className="product-list">
        {products.map((product, index) => (
          <li key={product.id} className="product-item" style={{ display: 'flex', justifyContent: 'flex-end', gap: '0.5rem', alignItems: 'center' }}>
            {selectedProductIndex === index && (
              <span style={{ marginRight: '10px', fontWeight: 'bold' }}>{`Product ${index + 1}`}</span>
            )}
            <button onClick={() => handleEdit(product, index)}>Edit</button>
            <button onClick={() => handleDelete(product.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </>
  )
}

export default Product
